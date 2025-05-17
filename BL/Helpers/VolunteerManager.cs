
using DalApi;

namespace Helpers;

internal static class VolunteerManager
{
    private static IDal s_dal = Factory.Get; //stage 4
    internal static ObserverManager Observers = new(); //stage 5 
    internal static (double lat, double lon) ValidateVolunteer(BO.Volunteer volunteer)
    {
        if (!IsValidIsraeliID(volunteer.Id.ToString()))
            throw new BO.BlValidationException("ID is invalid.");
        if (string.IsNullOrWhiteSpace(volunteer.Name))
            throw new BO.BlValidationException("Username is invalid.");
        if (!IsValidEmail(volunteer.Email))
            throw new BO.BlValidationException("Email address is invalid.");
        if (!IsValidPhone(volunteer.PhoneNumber))
            throw new BO.BlValidationException("Phone number is invalid.");
        (double lat, double lon) = isValidAddress(volunteer.CurrentAddress);
        if (lat == null || lon == null)
            throw new BO.BlValidationException("The address does not exist");
        return (lat, lon);
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
    internal static (double lat, double lon) isValidAddress(string address)
    {
        return CallManager.GetCoordinates(address);
    }
    internal static bool IsValidEmail(string email) =>
    new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
    internal static bool IsValidPhone(string phone) =>
    phone.All(char.IsDigit) && phone.Length == 10;
}
