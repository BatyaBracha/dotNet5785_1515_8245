
using BO;
using DalApi;
using DO;
using System.Net;
using System.Text.Json;

namespace Helpers;

internal static class CallManager
{
    private static IDal s_dal = Factory.Get; //stage 4
    internal static ObserverManager Observers = new(); //stage 5 
    private static BO.Status CallStatus(int callId) { return CallManager.CallStatus(callId); }

    //private class Coordinates
    //{
    //    public double lat { get; set; }
    //    public double lon { get; set; }
    //}
    public static double GetAerialDistance(string VolunteerAddress, string CallAddress)
    {
        var (volunteerLatitude, volunteerLongitude) = GetCoordinates(VolunteerAddress);
        var (callLatitude, callLongitude) = GetCoordinates(CallAddress);
        return CalculateDistance(volunteerLatitude, volunteerLongitude, callLatitude, callLongitude).Value;
    }
    public static double? CalculateDistance(object latitude1, object longitude1, double? latitude2, double? longitude2)
    {
        // המרת פרמטרים מסוג object ל-double
        if (!double.TryParse(latitude1?.ToString(), out double lat1) ||
            !double.TryParse(longitude1?.ToString(), out double lon1))
        {
            throw new ArgumentException("Invalid latitude or longitude values.");
        }
        if (!latitude2.HasValue || !longitude2.HasValue)

            return null;

        const double EarthRadiusKm = 6371; // רדיוס כדור הארץ בקילומטרים

        // המרת מעלות לרדיאנים
        double lat1Rad = DegreesToRadians(lat1);
        double lon1Rad = DegreesToRadians(lon1);
        double lat2Rad = DegreesToRadians(latitude2.Value);
        double lon2Rad = DegreesToRadians(longitude2.Value);

        // חישוב ההפרשים
        double deltaLat = lat2Rad - lat1Rad;
        double deltaLon = lon2Rad - lon1Rad;

        // נוסחת Haversine
        double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                   Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                   Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        // חישוב המרחק
        double distance = EarthRadiusKm * c;
        return distance;
    }

    private static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
    public static (double latitude, double longitude) GetCoordinates(string address)
    {
        string apiKey = "PK.83B935C225DF7E2F9B1ee90A6B46AD86";
        using var client = new HttpClient();
        string url = $"https://us1.locationiq.com/v1/search.php?key={apiKey}&q={Uri.EscapeDataString(address)}&format=json";

        var response = client.GetAsync(url).GetAwaiter().GetResult();
        if (!response.IsSuccessStatusCode)
            throw new BlArgumentException("Invalid address or API error.");

        var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        using var doc = JsonDocument.Parse(json);

        if (doc.RootElement.ValueKind != JsonValueKind.Array || doc.RootElement.GetArrayLength() == 0)
            throw new BlArgumentException("Address not found.");

        var root = doc.RootElement[0];

        double latitude = double.Parse(root.GetProperty("lat").GetString());
        double longitude = double.Parse(root.GetProperty("lon").GetString());

        return (latitude, longitude);
    }


    public static void PeriodicCallsUpdates()
    {
        var calls = s_dal.Call.ReadAll();
        var assignments = s_dal.Assignment.ReadAll();
        DateTime systemTime = AdminManager.Now;

        var expiredCalls = calls
            .Where(call => call.MaxClosingTime < systemTime)
            .ToList();

        var callsWithoutAssignments = expiredCalls
            .Where(call => !assignments.Any(a => a.CallId == call.Id))
            .Select(call => new Assignment
            {
                CallId = call.Id,
                VolunteerId = 0,
                TreatmentStartTime = call.MaxClosingTime ?? systemTime,
                TreatmentEndTime = systemTime,
                TypeOfTreatmentEnding = DO.TypeOfTreatmentEnding.EXPIRED
            })
            .ToList();

        callsWithoutAssignments.ForEach(newAssignment => s_dal.Assignment.Create(newAssignment));

        var assignmentsToUpdate = expiredCalls
            .SelectMany(call => assignments.Where(a => a.CallId == call.Id && a.TreatmentEndTime == null))
            .ToList();

        assignmentsToUpdate.ForEach(assignment =>
        {
            var updatedAssignment = assignment with
            {
                TreatmentEndTime = systemTime,
                TypeOfTreatmentEnding = DO.TypeOfTreatmentEnding.EXPIRED
            };
            s_dal.Assignment.Update(updatedAssignment);
        });
    }

    //private static void SimulateCallAssignment()
    //{
    //    // קריאת רשימות מתנדבים והקצאות
    //    var volunteers = s_dal.Volunteer.ReadAll();
    //    var assignments = s_dal.Assignment.ReadAll();
    //    var calls = s_dal.Call.ReadAll();
    //    DateTime systemTime = ClockManager.Now;

    //    // סינון מתנדבים שאין להם הקצאת קריאה נוכחית
    //    var availableVolunteers = volunteers
    //        .Where(volunteer => !assignments.Any(a => a.VolunteerId == volunteer.Id && a.TreatmentEndTime == null))
    //        .ToList();

    //    // ביצוע הלוגיקה: XXX
    //    foreach (var volunteer in availableVolunteers)
    //    {
    //        // כאן תתבצע לוגיקה עתידית לבחירת קריאה למתנדב (תעדכן בשלב 7)
    //        var suitableCall = calls
    //            .Where(call => call.MaxClosingTime > systemTime) // קריאות בתוקף
    //            .OrderBy(call => call.OpeningTime) // לדוגמה: סדר לפי זמן פתיחה
    //            .FirstOrDefault();

    //        if (suitableCall != null)
    //        {
    //            // יצירת הקצאה למתנדב
    //            var newAssignment = new Assignment
    //            {
    //                CallId = suitableCall.Id,
    //                VolunteerId = volunteer.Id,
    //                TreatmentStartTime = systemTime,
    //                TreatmentEndTime = null,
    //                TypeOfTreatmentEnding = null
    //            };

    //            // הוספת ההקצאה ל-DAL
    //            s_dal.Assignment.Create(newAssignment);
    //        }
    //    }
    //}



}
