
using DalApi;
using DO;
using System.ComponentModel.DataAnnotations;

namespace Helpers;

internal static class VolunteerManager
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    private static IDal s_dal = Factory.Get; //stage 4
    internal static ObserverManager Observers = new(); //stage 5 
    internal static void ValidateVolunteer(BO.Volunteer volunteer)
    {
        if (!IsValidIsraeliID(volunteer.Id.ToString()))
            throw new BO.BlValidationException("ID is invalid.");
        if (string.IsNullOrWhiteSpace(volunteer.Name))
            throw new BO.BlValidationException("Username is invalid.");
        if (!IsValidEmail(volunteer.Email))
            throw new BO.BlValidationException("Email address is invalid.");
        if (!IsValidPhone(volunteer.PhoneNumber))
            throw new BO.BlValidationException("Phone number is invalid.");
        if (!IsValidPassword(volunteer.Password))
            throw new BO.BlValidationException("Password is invalid.");
        //(double lat, double lon) = isValidAddress(volunteer.CurrentAddress);
        //if (lat == null || lon == null)
        //    throw new BO.BlValidationException("The address does not exist");
        //return (lat, lon);
    }
    public static bool IsValidPassword(string password)
    {
        if (password.Length == 64)
            return true;

        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (password.Length < 8)
            return false;

        if (!password.Any(char.IsUpper))
            return false;

        if (!password.Any(char.IsLower))
            return false;

        if (!password.Any(char.IsDigit))
            return false;

        if (!password.Any(ch => "!@#$%^&*()-_=+[]{};:'\",.<>?/\\|`~".Contains(ch)))
            return false;

        return true;
    }
    internal static bool IsValidIsraeliID(string id)
    {
        id = id.Replace("-", "").Trim();

        if (id.Length != 9)
        {
            return false;
        }

        int sum = 0;

        for (int i = 0; i < id.Length; i++)
        {
            int digit = int.Parse(id[id.Length - 1 - i].ToString());

            if (i % 2 == 1)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit -= 9;
                }
            }

            sum += digit;
        }
        return sum % 10 == 0;
    }
    internal static bool IsValidEmail(string email) =>
    new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
    internal static bool IsValidPhone(string phone) =>
    phone.All(char.IsDigit) && phone.Length == 10;
    //internal static void VolunteerActivitySimulation()
    //{
    //    try
    //    {
    //        // Fetch all active volunteers as a concrete list
    //        List<DO.Volunteer> activeVolunteers;
    //        lock (AdminManager.BlMutex)
    //        {
    //            activeVolunteers = s_dal.Volunteer.ReadAll(v => v.Active).ToList();
    //        }

    //        // List to collect IDs for notifications
    //        List<int> updatedVolunteerIds = new();

    //        foreach (var volunteer in activeVolunteers)
    //        {
    //            try
    //            {
    //                // Lock for each volunteer's operations
    //                lock (AdminManager.BlMutex)
    //                {
    //                    // Check if the volunteer has a call in progress
    //                    var assignment = s_dal.Assignment.ReadAll(a => a.VolunteerId == volunteer.Id && a.TreatmentEndTime == null).FirstOrDefault();

    //                    if (assignment == null)
    //                    {
    //                        // Volunteer has no call in progress
    //                        // Fetch open calls that are already calculated with coordinates
    //                        var openCallsList = s_bl.Call.GetOpenCallsCanBeSelectedByAVolunteer(volunteer.Id, null, null, null).ToList();

    //                        // Randomly decide whether to assign a call (e.g., 20% probability)
    //                        if (new Random().Next(100) < 20 && openCallsList.Any())
    //                        {
    //                            // Randomly select a call
    //                            var selectedCall = openCallsList[new Random().Next(openCallsList.Count)];
    //                            s_bl.Call.ChoosingACallForTreatment(volunteer.Id, selectedCall.Id);
    //                            updatedVolunteerIds.Add(volunteer.Id); // Collect ID for notification
    //                        }
    //                    }
    //                    else
    //                    {
    //                        // Volunteer has a call in progress
    //                        var timeElapsed = AdminManager.Now - assignment.TreatmentStartTime;
    //                        var call = s_bl.Call.GetCallDetails(assignment.CallId); // Ensure the call details are loaded
    //                        var distance = CallManager.GetAerialDistanceByCoordinates(volunteer.latitude, volunteer.longitude, call.Latitude, call.Longitude);

    //                        // Decide if "enough time" has passed to complete the call
    //                        var randomTimeFactor = TimeSpan.FromMinutes(new Random().Next(5, 15)); // Random time factor
    //                        if (timeElapsed.TotalMinutes > distance / 10 + randomTimeFactor.TotalMinutes)
    //                        {
    //                            s_bl.Call.TreatmentCompletionUpdate(volunteer.Id, assignment.Id);
    //                            updatedVolunteerIds.Add(volunteer.Id); // Collect ID for notification
    //                        }
    //                        else if (new Random().Next(100) < 10)
    //                        {
    //                            s_bl.Call.TreatmentCancellationUpdate(volunteer.Id, assignment.Id);
    //                            updatedVolunteerIds.Add(volunteer.Id); // Collect ID for notification
    //                        }
    //                    }
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                throw new BO.BlUnauthorizedOperationException($"Error processing volunteer {volunteer.Id}: {ex.Message}");
    //            }
    //        }

    //        // Notify observers outside the lock
    //        updatedVolunteerIds.ForEach(id => Observers.NotifyItemUpdated(id));
    //    }
    //    catch (Exception ex)
    //    {
    //       throw new BO.BlUnauthorizedOperationException($"Simulation failed: {ex.Message} {ex}");
    //    }
    //}
    private static readonly Random s_rand = new();
    private static int s_simulatorCounter = 0;

    internal static void VolunteerActivitySimulation()
    {
        //Thread.CurrentThread.Name = $"Simulator{++s_simulatorCounter}";
        List<DO.Volunteer> doVolunteerList;
        lock (AdminManager.BlMutex) //stage 7
            doVolunteerList = s_dal.Volunteer.ReadAll(volunteer => volunteer.Active == true).ToList();
        foreach (var doVolunter in doVolunteerList)
        {
            int volunteerId = 0;
            lock (AdminManager.BlMutex) //stage 7
            {
                BO.Volunteer currentVolunteer = s_bl.Volunteer.Read(doVolunter.Id);
                if (currentVolunteer.CallInProgress == null)
                {
                    var openCallsList = s_bl.Call.GetOpenCallsCanBeSelectedByAVolunteer(currentVolunteer.Id, null, null, null).ToList();
                    if (new Random().Next(100) < 20 && openCallsList.Any())
                    {
                        var selectedCall = openCallsList[s_rand.Next(openCallsList.Count)];
                        s_bl.Call.ChoosingACallForTreatment(currentVolunteer.Id, selectedCall.Id);
                    }
                }
                else
                {
                    var timeElapsed = AdminManager.Now - currentVolunteer.CallInProgress.TimeOfEntryToTreatment;
                    var distance = currentVolunteer.CallInProgress.CallVolunteerDistance;
                    var randomTimeFactor = TimeSpan.FromMinutes(s_rand.Next(5, 15));
                    if (timeElapsed.TotalMinutes > distance + randomTimeFactor.TotalMinutes)
                    {
                        s_bl.Call.TreatmentCompletionUpdate(currentVolunteer.Id, currentVolunteer.CallInProgress.Id);
                    }
                    else
                    {
                        if (s_rand.Next(100) < 10)
                        {
                            s_bl.Call.TreatmentCancellationUpdate(currentVolunteer.Id, currentVolunteer.CallInProgress.Id);
                        }

                    }
                }
            }
        }
    }
}
