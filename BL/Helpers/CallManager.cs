
using BO;
using DalApi;
using System.Net;
using System.Text.Json;

namespace Helpers;

internal static class CallManager
{
    private static IDal s_dal = Factory.Get; //stage 4
    private static Status CallStatus(int callId) { return CallManager.CallStatus(callId); }
    internal static (double Latitude, double Longitude) GetCoordinates(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("הכתובת שסופקה ריקה או לא תקינה.");

        string url = $"https://geocode.maps.co/search?q={Uri.EscapeDataString(address)}";

        try
        {
            // שליחת בקשת GET סינכרונית
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"שגיאה בבקשת הרשת: {response.StatusCode}");

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string jsonResponse = reader.ReadToEnd();

                    // ניתוח תגובת ה-JSON
                    //var results = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                    var results = JsonSerializer.Deserialize<dynamic>(jsonResponse);

                    if (results == null || results.Count == 0)
                        throw new ArgumentException("לא נמצאו קואורדינטות לכתובת שסופקה.");

                    // החזרת הקואורדינטות הראשונות מהתוצאות
                    double latitude = double.Parse((string)results[0].lat);
                    double longitude = double.Parse((string)results[0].lon);

                    return (latitude, longitude);
                }
            }
        }
        catch (WebException ex)
        {
            throw new Exception("שגיאה בחיבור לאתר השירות: " + ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new Exception("שגיאה כללית במהלך חישוב הקואורדינטות: " + ex.Message, ex);
        }
    }
}
