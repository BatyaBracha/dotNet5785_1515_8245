
using BlApi;
using BO;
using DalApi;
using DO;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

namespace Helpers;

internal static class CallManager
{
    private static IDal s_dal = DalApi.Factory.Get; //stage 4
    internal static ObserverManager Observers = new(); //stage 5 
    private static BO.Status CallStatus(int callId) { return CallManager.CallStatus(callId); }
    internal static void ValidateCall(BO.Call boCall)
    {
        if (boCall == null)
            throw new BlNullPropertyException("The call object cannot be null.", null);

        // Check if the ID is valid
        if (boCall.Id < 0)
            throw new BlArgumentException("Call ID must be a positive number.");

        // Validate time constraints
        //if (boCall.MaxClosingTime.HasValue && boCall.MaxClosingTime.Value <= boCall.OpeningTime)
        //    throw new BlArgumentException("End time must be greater than the start time.");

        // Check if the address is valid
        if (string.IsNullOrWhiteSpace(boCall.Address))
            throw new BlArgumentException("Address cannot be null or empty.");

        // Validate the address against a geolocation service
        //(double? lat, double? lon) = await GetCoordinates(boCall.Address);

        //if (lat == null || lon == null)
        //    throw new BlArgumentException("Address is invalid or could not be found.");

        //// Assign valid coordinates
        //boCall.Latitude = lat;
        //boCall.Longitude = lon;

        // Check for other business logic conditions if needed
        if (boCall.Status == BO.CallStatus.CLOSED && boCall.OpeningTime > DateTime.Now)
            throw new BlArgumentException("A closed call cannot have a start time in the future.");
    }





    //internal static bool MatchField(Enum? filterBy, DO.Call call, object? filterValue)
    //{
    //    if (filterBy == null)
    //        return true;

    //    // Match by the field defined in the filterBy enum
    //    switch (filterBy)
    //    {
    //        case BO.CallField.STATUS:
    //            return (BO.CallStatus)call.Status == (BO.CallStatus)Enum.Parse(typeof(BO.CallStatus), filterValue.ToString());

    //        //case BO.CallField.STATUS:
    //        //    return call.Status.Equals(filterValue);
    //        //case BO.CallField.PRIORITY:
    //        //    return call.riskRange.Equals(filterValue);
    //        case BO.CallField.TYPE:
    //            return (BO.TypeOfCall)call.TypeOfCall == (BO.TypeOfCall)Enum.Parse(typeof(BO.TypeOfCall), filterValue.ToString(), true);
    //        default:
    //            throw new BlNullPropertyException("Unsupported filter field", nameof(filterBy));
    //    }
    //}
    //public static bool MatchField(Enum filterBy, object item, object filterValue)
    //{
    //    // Use reflection to get the property specified by the filterBy enum
    //    var propertyName = filterBy.ToString();
    //    var property = item.GetType().GetProperty(propertyName);

    //    if (property == null)
    //        throw new ArgumentException($"Property '{propertyName}' not found on type '{item.GetType().Name}'.");

    //    var propertyValue = property.GetValue(item);

    //    // Handle Enum comparison
    //    if (propertyValue is Enum && filterValue is string)
    //    {
    //        // Convert the string filterValue to the corresponding Enum type
    //        var enumType = propertyValue.GetType();
    //        if (Enum.TryParse(enumType, filterValue.ToString(), out var parsedEnum))
    //        {
    //            return propertyValue.Equals(parsedEnum);
    //        }
    //        return false; // If parsing fails, return false
    //    }

    //    // Compare the property value with the filter value
    //    return propertyValue != null && propertyValue.Equals(filterValue);
    //}
    public static bool MatchField(Enum filterBy, object item, object filterValue)
    {
        var propertyName = filterBy.ToString();
        var property = item.GetType().GetProperty(propertyName);

        if (property == null)
            throw new ArgumentException($"Property '{propertyName}' not found on type '{item.GetType().Name}'.");

        var propertyValue = property.GetValue(item);
        if (propertyValue == null || filterValue == null)
            return false;

        // אם שני הערכים הם Enum – נשווה ישירות
        //if (propertyValue is Enum && filterValue is Enum)
        //    return propertyValue.Equals(filterValue);

        // אם טיפוסים שונים – ננסה להשוות כ־string
        return propertyValue != null && propertyValue.ToString() == filterValue.ToString();
    }

    //public static bool MatchFieldExtended(Enum filterBy, DO.Call call, DO.Assignment assignment, object filterValue)
    //{
    //    var propertyName = filterBy.ToString();

    //    // ננסה קודם ב־Call
    //    var property = call.GetType().GetProperty(propertyName);
    //    object? propertyValue = null;

    //    if (property != null)
    //        propertyValue = property.GetValue(call);
    //    else
    //    {
    //        // ננסה ב־Assignment
    //        property = assignment.GetType().GetProperty(propertyName);
    //        if (property != null)
    //            propertyValue = property.GetValue(assignment);
    //        else
    //            throw new ArgumentException($"Property '{propertyName}' not found on Call or Assignment.");
    //    }

        // Compare the property value with the filter value
    //    return propertyValue != null && propertyValue.ToString()==filterValue.ToString();
    //}

    internal static IEnumerable<T> SortByField<T>(Enum? sortBy, IEnumerable<T> items)
    {
        if (sortBy == null)
            return items.OrderBy(c => (c as dynamic).Id); // Default sort by ID
        // Status,TypeOfCall, ADDRESS, CALL_VOLUNTEER_DISTANCE,ID ,None
        // Sort by the field defined in the sortBy enum
        return sortBy switch
        {
            BO.CallInListField.TypeOfCall => items.OrderBy(c => (c as dynamic).TypeOfCall),
            BO.CallInListField.Status => items.OrderBy(c => (c as dynamic).Status),
            //BO.CallInListField.ADDRESS => items.OrderBy(c => (c as dynamic).Adress),
            //BO.CallInListField.CALL_VOLUNTEER_DISTANCE => items.OrderBy(c => (c as dynamic).CALL_VOLUNTEER_DISTANCE),
            //BO.CallInListField.ID => items.OrderBy(c => (c as dynamic).ID),
            BO.CallInListField.None => items.OrderBy(c => (c as dynamic).Id), // Default sort by ID
            _ => throw new BlNullPropertyException("Unsupported sort field", nameof(sortBy)),
        };
    }
    internal static BO.CallStatus CalculateCallStatus(IEnumerable<DO.Assignment> assignments, DateTime? maxClosingTime)
    {
        // Retrieve the risk range and system time
        TimeSpan riskRange = AdminManager.RiskRange; // Get the risk range from the configuration
        DateTime systemTime = AdminManager.Now; // Get the current system time

        // Check if the call has any active assignments
        var activeAssignment = assignments.FirstOrDefault(a => a.TreatmentEndTime == null);

        // Case 1: Call is currently being handled
        if (activeAssignment != null)
        {
            // Check if the call is in the "risk range"
            if (maxClosingTime.HasValue && (maxClosingTime.Value - systemTime) <= riskRange)
            {
                return BO.CallStatus.BEING_HANDELED_IN_RISK;
            }
            return BO.CallStatus.BEING_HANDELED;
        }

        // Case 2: Call has been closed (handled by a volunteer)
        var closedAssignment = assignments.FirstOrDefault(a => a.TypeOfTreatmentEnding.HasValue && (a.AssignmentStatus == DO.AssignmentStatus.CLOSED)|| a.AssignmentStatus == DO.AssignmentStatus.COMPLETED);
        if (closedAssignment != null)
        {
            return BO.CallStatus.CLOSED;
        }

        // Case 3: Call has expired
        if (maxClosingTime.HasValue && maxClosingTime.Value < systemTime)
        {
            return BO.CallStatus.OUT_OF_DATE;
        }

        // Case 4: Call is open and in the "risk range"
        if (maxClosingTime.HasValue && (maxClosingTime.Value - systemTime) <= riskRange)
        {
            return BO.CallStatus.OPEN_IN_RISK;
        }

        // Case 5: Call is open
        return BO.CallStatus.OPEN;
    }

    //public static double GetAerialDistance(string VolunteerAddress, string CallAddress)
    //{
    //    //if (string.IsNullOrWhiteSpace(CallAddress)|| string.IsNullOrWhiteSpace(VolunteerAddress))
    //    //    throw new BO.BlArgumentException("Address is required for geocoding.");
    //    var (volunteerLatitude, volunteerLongitude) = GetCoordinates(VolunteerAddress);
    //    var (callLatitude, callLongitude) = GetCoordinates(CallAddress);
    //    return CalculateDistance(volunteerLatitude, volunteerLongitude, callLatitude, callLongitude).Value;
    //}

    public static double GetAerialDistanceByCoordinates(double? volunteerLatitude,double? volunteerLongitude,double? callLatitude, double? callLongitude)
    {
        //if (string.IsNullOrWhiteSpace(CallAddress)|| string.IsNullOrWhiteSpace(VolunteerAddress))
        //    throw new BO.BlArgumentException("Address is required for geocoding.");
        return CalculateDistance(volunteerLatitude, volunteerLongitude, callLatitude, callLongitude);
    }

    public static double CalculateDistance(object latitude1, object longitude1, double? latitude2, double? longitude2)
    {
        // המרת פרמטרים מסוג object ל-double
        if (!double.TryParse(latitude1?.ToString(), out double lat1) ||
            !double.TryParse(longitude1?.ToString(), out double lon1))
        {
            throw new ArgumentException("Invalid latitude or longitude values.");
        }
        if (!latitude2.HasValue || !longitude2.HasValue)
            throw new BlNullPropertyException("latitude or longitude doesnt exist","");

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
    public static async Task<(double latitude, double longitude)> GetCoordinates(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new BO.BlArgumentException("Address is required for geocoding.");
        //string apiKey = "AIzaSyBW85eFbcSZizuOdBkW8ymPZlKhxaq_EFs";
        string apiKey = "PK.83B935C225DF7E2F9B1ee90A6B46AD86";
        using var client = new HttpClient();
        //string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={apiKey}";
        string url = $"https://us1.locationiq.com/v1/search.php?key={apiKey}&q={Uri.EscapeDataString(address)}&format=json";

        var response = await client.GetAsync(url);
        // Log the response status code and content
        //string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        if (!response.IsSuccessStatusCode)
            throw new BlArgumentException("Invalid address or API error.");

        var json =await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);

        if (doc.RootElement.ValueKind != JsonValueKind.Array || doc.RootElement.GetArrayLength() == 0)
            throw new BlArgumentException("Address not found.");

        var root = doc.RootElement[0];

        double latitude = double.Parse(root.GetProperty("lat").GetString());
        double longitude = double.Parse(root.GetProperty("lon").GetString());

        return (latitude, longitude);

        //using var doc = JsonDocument.Parse(responseContent);
        //var root = doc.RootElement;

        //var status = root.GetProperty("status").GetString();
        //if (status != "OK")
        //    throw new Exception($"Google Maps returned error: {status}");

        //var location = root
        //    .GetProperty("results")[0]
        //    .GetProperty("geometry")
        //    .GetProperty("location");

        //double lat = location.GetProperty("lat").GetDouble();
        //double lng = location.GetProperty("lng").GetDouble();

        //return (lat, lng);
    }
    //public static (double latitude, double longitude) GetCoordinates(string address)
    //{
    //    if (string.IsNullOrWhiteSpace(address))
    //        throw new BO.BlArgumentException("Address is required for geocoding.");

    //    // Use MemoryCache to check if the coordinates are already cached
    //    var cache = MemoryCache.Default;
    //    if (cache[address] is (double latitude, double longitude) cachedCoordinates)
    //    {
    //        return cachedCoordinates;
    //    }

    //    // Proceed with the API call if not cached
    //    string apiKey = "PK.83B935C225DF7E2F9B1ee90A6B46AD86";
    //    using var client = new HttpClient();
    //    string url = $"https://us1.locationiq.com/v1/search.php?key={apiKey}&q={Uri.EscapeDataString(address)}&format=json";

    //    var response = client.GetAsync(url).GetAwaiter().GetResult();
    //    if (!response.IsSuccessStatusCode)
    //        throw new BlArgumentException("Invalid address or API error.");

    //    var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
    //    using var doc = JsonDocument.Parse(json);

    //    if (doc.RootElement.ValueKind != JsonValueKind.Array || doc.RootElement.GetArrayLength() == 0)
    //        throw new BlArgumentException("Address not found.");

    //    var root = doc.RootElement[0];
    //    latitude = double.Parse(root.GetProperty("lat").GetString());
    //    longitude = double.Parse(root.GetProperty("lon").GetString());

    //    // Cache the result for future use
    //    cache.Set(address, (latitude, longitude), DateTimeOffset.Now.AddHours(1)); // Cache for 1 hour

    //    return (latitude, longitude);
    //}


    //public static void PeriodicCallsUpdates(DateTime systemTime)
    //{
    //    IEnumerable<DO.Call> calls;
    //    IEnumerable<DO.Assignment> assignments;
    //    lock (AdminManager.BlMutex) { 
    //         calls = s_dal.Call.ReadAll();
    //         assignments = s_dal.Assignment.ReadAll();
    //    }
    //    //DateTime systemTime = AdminManager.Now;

    //    var expiredCalls = calls
    //        .Where(call => call.MaxClosingTime < systemTime)
    //        .ToList();

    //    var callsWithoutAssignments = expiredCalls
    //        .Where(call => !assignments.Any(a => a.CallId == call.Id))
    //        .Select(call => new Assignment
    //        {
    //            CallId = call.Id,
    //            VolunteerId = 0,
    //            TreatmentStartTime = call.MaxClosingTime ?? systemTime,
    //            TreatmentEndTime = systemTime,
    //            TypeOfTreatmentEnding = DO.TypeOfTreatmentEnding.EXPIRED_CANCELED
    //        })
    //        .ToList();

    //    callsWithoutAssignments.ForEach(newAssignment => s_dal.Assignment.Create(newAssignment));

    //    var assignmentsToUpdate = expiredCalls
    //        .SelectMany(call => assignments.Where(a => a.CallId == call.Id && a.TreatmentEndTime == null))
    //        .ToList();

    //    assignmentsToUpdate.ForEach(assignment =>
    //    {
    //        var updatedAssignment = assignment with
    //        {
    //            TreatmentEndTime = systemTime,
    //            TypeOfTreatmentEnding = DO.TypeOfTreatmentEnding.EXPIRED_CANCELED
    //        };
    //        s_dal.Assignment.Update(updatedAssignment);
    //    });
    //}
    internal static async Task UpdateCoordinatesForCallAsync(int id,bool create)
    {
        //if (!string.IsNullOrWhiteSpace(doCall.Address))
        //{
            try
            {
            DO.Call doCall;
            lock (AdminManager.BlMutex)
            {
                doCall= s_dal.Call.Read(id)!; // Update the call in the DAL
            }
            // Fetch coordinates asynchronously
            var (latitude, longitude) = await GetCoordinates(doCall!.Address);

                // Update the call with the fetched coordinates
                doCall = doCall with { latitude = latitude, longitude = longitude };

                lock (AdminManager.BlMutex)
                {
                    s_dal.Call.Update(doCall); // Update the call in the DAL
                }
                BO.Call TypeOfCall=null;
                if (create) {
                    // If this is a new call, send an email notification
                     TypeOfCall = new BO.Call(doCall.Id, (BO.TypeOfCall)doCall.TypeOfCall, doCall.Description, doCall.Address, doCall.latitude, doCall.longitude, doCall.OpeningTime, doCall.MaxClosingTime, BO.CallStatus.OPEN,null);
                     //SendEmailWhenCallOpened(TypeOfCall);
                    }
                // Notify observers about the update
                CallManager.Observers.NotifyItemUpdated(doCall.Id);
                CallManager.Observers.NotifyListUpdated();
                SendEmailWhenCallOpened(TypeOfCall);
            }
            catch (Exception ex)
            {
                throw new BlArgumentException(ex.Message);           
            }
        //}
    }
    internal static async Task UpdateCoordinatesForVolunteerAsync(DO.Volunteer doVolunteer)
    {
        if (!string.IsNullOrWhiteSpace(doVolunteer.Address))
        {
            try
            {
                // Fetch coordinates asynchronously
                var (latitude, longitude) = await GetCoordinates(doVolunteer.Address);

                // Update the call with the fetched coordinates
                doVolunteer = doVolunteer with { latitude = latitude, longitude = longitude };

                lock (AdminManager.BlMutex)
                {
                    s_dal.Volunteer.Update(doVolunteer); // Update the call in the DAL
                }

                // Notify observers about the update
                VolunteerManager.Observers.NotifyItemUpdated(doVolunteer.Id);
                VolunteerManager.Observers.NotifyListUpdated();
            }
            catch (Exception ex)
            {
                throw new BlArgumentException(ex.Message);
            }
        }
    }

    public static void PeriodicCallsUpdates(DateTime systemTime)
    {
        // Fetch all calls and assignments as concrete lists to avoid deferred execution
        List<DO.Call> calls;
        List<DO.Assignment> assignments;

        lock (AdminManager.BlMutex)
        {
            calls = s_dal.Call.ReadAll().ToList();
            assignments = s_dal.Assignment.ReadAll().ToList();
        }

        // Filter expired calls
        var expiredCalls = calls
            .Where(call => call.MaxClosingTime < systemTime)
            .ToList();

        // Handle calls without assignments
        var callsWithoutAssignments = expiredCalls
            .Where(call => !assignments.Any(a => a.CallId == call.Id))
            .Select(call => new Assignment
            {
                CallId = call.Id,
                VolunteerId = 0,
                TreatmentStartTime = call.MaxClosingTime ?? systemTime,
                TreatmentEndTime = systemTime,
                TypeOfTreatmentEnding = DO.TypeOfTreatmentEnding.EXPIRED_CANCELED
            })
            .ToList();

        // Create assignments for calls without assignments
        List<int> createdAssignmentIds = new();
        callsWithoutAssignments.ForEach(newAssignment =>
        {
            lock (AdminManager.BlMutex)
            {
                s_dal.Assignment.Create(newAssignment);
                createdAssignmentIds.Add(newAssignment.CallId); // Collect IDs for notification
            }
        });

        // Update assignments for expired calls
        var assignmentsToUpdate = expiredCalls
            .SelectMany(call => assignments.Where(a => a.CallId == call.Id && a.TreatmentEndTime == null))
            .ToList();

        List<int> updatedAssignmentIds = new();
        assignmentsToUpdate.ForEach(assignment =>
        {
            var updatedAssignment = assignment with
            {
                TreatmentEndTime = systemTime,
                TypeOfTreatmentEnding = DO.TypeOfTreatmentEnding.EXPIRED_CANCELED
            };

            lock (AdminManager.BlMutex)
            {
                s_dal.Assignment.Update(updatedAssignment);
                updatedAssignmentIds.Add(updatedAssignment.CallId); // Collect IDs for notification
            }
        });

        // Notify observers outside the lock
        createdAssignmentIds.ForEach(id => Observers.NotifyItemUpdated(id));
        updatedAssignmentIds.ForEach(id => Observers.NotifyItemUpdated(id));
    }

    internal static void SendEmailWhenCallOpened(BO.Call call)
    {
        var volunteer = s_dal.Volunteer.ReadAll();
        foreach (var item in volunteer)
        {

            if (item.MaxDistance >= CalculateDistance(item.latitude!, item.longitude!, call.Latitude, call.Longitude))
            {
                string subject = "Openning call";
                string body = $@"
                                  Hello {item.Name},

                                  A new call has been opened in your area.
                                  Call Details:
                                  - Call Type: {call.TypeOfCall}
                                  - Call Address: {call.Address}
                                  - Opening Time: {call.OpeningTime}
                                  - Description: {call.Description}
                                  - Entry Time for Treatment: {call.MaxClosingTime}
                                  -call Status:{call.Status}
 
                                  If you wish to handle this call, please log into the system.
 
                                                              Best regards,  
                                                                      Call Management System";

                _=Tools.SendEmail(item.Email, subject, body);
            }
        }
    }
    /// <summary>
    /// Sends an email notification to the volunteer when their assignment is canceled.
    /// </summary>
    /// <param name="volunteer">The volunteer to notify.</param>
    /// <param name="assignment">The assignment that was canceled.</param>
    internal static void SendEmailToVolunteerWhenCallUnmatched(DO.Volunteer volunteer, DO.Assignment assignment)
    {
        var call = s_dal.Call.Read(assignment.CallId)!;

        string subject = "Assignment Canceled";
        string body = $@"
                      Hello {volunteer.Name},

                      Your assignment for handling call {assignment.Id} has been canceled by the administrator.

                      Call Details:
                      - Call ID: {assignment.CallId}
                      - Call Type: {call.TypeOfCall}
                      - Call Address: {call.Address}
                      - Opening Time: {call.OpeningTime}
                      - Description: {call.Description}
                      - Entry Time for Treatment: {assignment.TreatmentStartTime}

                                                                                        Best regards,  
                                                                                        Call Management System";

        _=Tools.SendEmail(volunteer.Email, subject, body);
    }

    internal static async Task SendEmailWhenCallOpenedAsync(BO.Call call)
    {
        var volunteers = s_dal.Volunteer.ReadAll();
        var emailTasks = new List<Task>();

        foreach (var item in volunteers)
        {
            //if (item.MaxDistance >= CalculateDistance(item.latitude!, item.longitude!, call.Latitude, call.Longitude))
            
            {
                string subject = "Opening call";
                string body = $@" Hello {item.Name},
                                  
                                  A new call has been opened in your area.
                                  Call Details:
                                  - Call Type: {call.TypeOfCall}
                                  - Call Address: {call.Address}
                                  - Opening Time: {call.OpeningTime}
                                  - Description: {call.Description}
                                  - Entry Time for Treatment: {call.MaxClosingTime}
                                  - Call Status: {call.Status}
                                  
                                  If you wish to handle this call, please log into the system.
                                  
                                  Best regards,  
                                  Call Management System";

                emailTasks.Add(Tools.SendEmail(item.Email, subject, body));
            }
        }

        await Task.WhenAll(emailTasks); // שליחה מקבילית
    }



}
