
using BO;
using DalApi;
using System.Net;
using System.Text.Json;

namespace Helpers;

internal static class CallManager
{
    private static IDal s_dal = Factory.Get; //stage 4

    private static Status CallStatus(int callId) { return CallManager.CallStatus(callId); }

    private class Coordinates
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    private static (double Latitude, double Longitude) GetCoordinates(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address cannot be null or empty.");
        try
        {
            using (HttpClient client = new HttpClient())
            {
                string baseUrl = "https://geocode.maps.co/search?";
                string requestUrl = $"{baseUrl}q={Uri.EscapeDataString(address)}";

                // Send the HTTP request and get the response
                HttpResponseMessage response = client.GetAsync(requestUrl).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Failed to retrieve data. HTTP Status: {response.StatusCode}");

                string responseContent = response.Content.ReadAsStringAsync().Result;

                // Deserialize the JSON response
                var results = JsonSerializer.Deserialize<Coordinates[]>(responseContent);

                if (results == null || results.Length == 0)
                    throw new Exception("No results found for the given address.");

                return (results[0].lat, results[0].lon);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error occurred while retrieving coordinates: {ex.Message}", ex);
        }
    }

    private static double GetAerialDistance(string address1, string address2)
    {
        (double lat1, double lon1) = GetCoordinates(address1);
        (double lat2, double lon2) = GetCoordinates(address2);
        const double EarthRadiusKm = 6371.0; // Earth's radius in kilometers

        // Convert degrees to radians
        double radiansLat1 = DegreesToRadians(lat1);
        double radiansLon1 = DegreesToRadians(lon1);
        double radiansLat2 = DegreesToRadians(lat2);
        double radiansLon2 = DegreesToRadians(lon2);

        // Calculate the differences
        double deltaLat = radiansLat2 - radiansLat1;
        double deltaLon = radiansLon2 - radiansLon1;

        // Apply the Haversine formula
        double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                   Math.Cos(radiansLat1) * Math.Cos(radiansLat2) *
                   Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return EarthRadiusKm * c;
    }

    private static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }

    private static void UpdatingExpiredReadings()
    {
        var calls = s_dal.Call.ReadAll();
        var assignments = s_dal.Assignment.ReadAll;
        DateTime systemTime = ClockManager.Now;
        // Filter calls where EndTime has passed and process them
        var expiredCalls = calls
            .Where(call =>call.MaxClosingTime < systemTime )
            .ToList();
    }

}
