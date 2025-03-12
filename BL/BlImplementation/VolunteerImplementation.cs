
using BlApi;
using BO;
using DO;
using Helpers;
using System.Data;
using System;
using System.Xml.Linq;
namespace BlImplementation;

internal class VolunteerImplementation : IVolunteer
{
    private readonly DalApi.IDal Volunteer_dal = DalApi.Factory.Get;
    public BO.Role Login(string username, string password) 
    {
        return 0;
    }

    public void Create(BO.Volunteer boVolunteer)
    {
        try
        {
            ValidateVolunteer(boVolunteer);
            var user = Volunteer_dal.Volunteer.ReadAll()
                .FirstOrDefault(u => u.Id == boVolunteer.Id);
            if (user != null || user.Password != boVolunteer.Password)
                throw new BO.BlArgumentException("שם המשתמש או הסיסמה אינם נכונים.");
            var doVolunteer = new DO.Volunteer
            {
                Id = boVolunteer.Id,
                Name = boVolunteer.Name,
                Phone = boVolunteer.PhoneNumber,
                Email = boVolunteer.Email,
                Password = boVolunteer.Password,
                Address = boVolunteer.CurrentAddress,
                latitude = boVolunteer.Latitude,
                longitude = boVolunteer.Longitude,
                Role = (DO.Role)boVolunteer.Role,
                Active = boVolunteer.Active,
                MaxDistance = boVolunteer.MaxDistance,
                TypeOfDistance = (DO.TypeOfDistance)boVolunteer.TypeOfDistance
            };

            Volunteer_dal.Volunteer.Create(doVolunteer);
        }
        catch (DO.DalUnauthorizedOperationException ex)
        {
            throw new BO.BlUnauthorizedOperationException("שגיאה בגישה לנתוני משתמשים.");
        }
    }

    private void ValidateVolunteer(BO.Volunteer volunteer)
    {
        if (string.IsNullOrWhiteSpace(volunteer.Name))
            throw new BO.BlValidationException("שם המתנדב אינו תקין.");
        if (!IsValidEmail(volunteer.Email))
            throw new BO.BlValidationException("כתובת האימייל אינה תקינה.");
        if (!IsValidPhone(volunteer.PhoneNumber))
            throw new BO.BlValidationException("מספר הטלפון אינו תקין.");
        if(!isValidAddress(volunteer.CurrentAddress))
            throw new BO.BlValidationException("this address does not exist")
    }

    private bool isValidAddress(string address)
    {

    }
    private bool IsValidEmail(string email) =>
        new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);

    private bool IsValidPhone(string phone) =>
        phone.All(char.IsDigit) && phone.Length == 10;

    public void Delete(int id)
    {
        try
        {
            var activeAssignments = Volunteer_dal.Assignment.ReadAll()
                .Where(a => a.VolunteerId == id && a.TreatmentEndTime == null)
                .ToList();

            if (activeAssignments.Any())
                throw new BO.BlInvalidOperationException("לא ניתן למחוק מתנדב שמטפל כרגע בקריאה.");

            Volunteer_dal.Volunteer.Delete(id);
        }
        catch (DO.DalUnauthorizedOperationException ex)
        {
            throw new BO.BlUnauthorizedOperationException("שגיאה במחיקת נתוני מתנדבים.");
        }
    }

    public void MatchVolunteerToCall(int volunteerId, int callId)
    {
        try
        {
            var existingAssignment = Volunteer_dal.Assignment.ReadAll()
                .FirstOrDefault(a => a.VolunteerId == volunteerId && a.TreatmentEndTime == null);

            if (existingAssignment != null)
                throw new BO.BlInvalidOperationException("לא ניתן להתאים מתנדב שכבר מטפל בקריאה אחרת.");

            var newAssignment = new DO.Assignment
            {
                VolunteerId = volunteerId,
                CallId = callId,
                TreatmentStartTime = ClockManager.Now
            };

            Volunteer_dal.Assignment.Create(newAssignment);
        }
        catch (DO.DalUnauthorizedOperationException ex)
        {
            throw new BO.BlUnauthorizedOperationException("שגיאה בהתאמת מתנדב לקריאה.");
        }
    }

    public BO.Volunteer? Read(int id)
    {
        try
        {
            var doVolunteer = Volunteer_dal.Volunteer.Read(id)
                ?? throw new BO.BlDoesNotExistException("המתנדב לא נמצא.");

            var assignment = Volunteer_dal.Assignment.ReadAll()
                .FirstOrDefault(a => a.VolunteerId == id && a.TreatmentEndTime == null);

            BO.CallInProgress? callInProgress = null;

            if (assignment != null)
            {
                var call = Volunteer_dal.Call.Read(assignment.CallId);
                callInProgress = new BO.CallInProgress
                {
                    CallId = call.Id,
                    Address = call.Address,
                    Description = call.Description
                };
            }

            return new BO.Volunteer
            (
                doVolunteer.Id,
                doVolunteer.Name,
                doVolunteer.Phone,
                doVolunteer.Email,
                doVolunteer.Password,
                doVolunteer.Address,
                doVolunteer.latitude,
                doVolunteer.longitude,
                (BO.Role)doVolunteer.Role,
                doVolunteer.Active,
                doVolunteer.MaxDistance,
                (BO.TypeOfDistance)doVolunteer.TypeOfDistance,
                0,
                0,
                0,
               callInProgress);
        }
        catch (DO.DalUnauthorizedOperationException ex)
        {
            throw new BO.BlUnauthorizedOperationException("שגיאה בגישה לנתוני מתנדבים.");
        }
    }

    public IEnumerable<BO.VolunteerInList> ReadAll(BO.Active? sort = null, BO.VolunteerFields? filter = null, object? value = null)
    {
        try
        {
            var volunteers = Volunteer_dal.Volunteer.ReadAll();

            // סינון לפי סטטוס
            if (sort.HasValue)
                volunteers = volunteers.Where(v => v.Active == (sort == BO.Active.TRUE)).ToList();

            var volunteerList = volunteers.Select(v => new BO.VolunteerInList
            {
                Id = v.Id,
                Name = v.Name,
                Active = v.Active
            });

            // מיון לפי שדה ספציפי
            if (filter.HasValue)
            {
                volunteerList = filter switch
                {
                    BO.VolunteerFields.Name => volunteerList.OrderBy(v => v.Name),
                    BO.VolunteerFields.Id => volunteerList.OrderBy(v => v.Id),
                    _ => volunteerList.OrderBy(v => v.Id)
                };
            }

            return volunteerList.ToList();
        }
        catch (DO.DalUnauthorizedOperationException ex)
        {
            throw new BO.BlUnauthorizedOperationException("שגיאה בגישה לנתוני מתנדבים.");
        }
    }

    public void UnMatchVolunteerToCall(int volunteerId, int callId)
    {
        try
        {
            var assignment = Volunteer_dal.Assignment.ReadAll()
                .FirstOrDefault(a => a.VolunteerId == volunteerId && a.CallId == callId && a.TreatmentEndTime == null);

            if (assignment == null)
                throw new BO.BlDoesNotExistException("לא נמצאה התאמה בין המתנדב לקריאה.");


            Volunteer_dal.Assignment.Update(new DO.Assignment(assignment.Id, assignment.CallId,assignment.VolunteerId, assignment.TreatmentStartTime,ClockManager.Now, DO.TypeOfTreatmentEnding.UNMATCHED,assignment.AssignmentStatus));
        }
        catch (DO.DalUnauthorizedOperationException ex)
        {
            throw new BO.BlUnauthorizedOperationException("שגיאה בביטול התאמת מתנדב לקריאה.");
        }
    }

    public void Update(BO.Volunteer boVolunteer)
    {
        try
        {
            ValidateVolunteer(boVolunteer);

            var doVolunteer = new DO.Volunteer
            {
                Id = boVolunteer.Id,
                Name = boVolunteer.Name,
                Phone = boVolunteer.PhoneNumber,
                Email = boVolunteer.Email,
                Password = boVolunteer.Password,
                Address = boVolunteer.CurrentAddress,
                latitude = boVolunteer.Latitude,
                longitude = boVolunteer.Longitude,
                Role = (DO.Role)boVolunteer.Role,
                Active = boVolunteer.Active,
                MaxDistance = boVolunteer.MaxDistance,
                TypeOfDistance = (DO.TypeOfDistance)boVolunteer.TypeOfDistance
            };

            Volunteer_dal.Volunteer.Update(doVolunteer);
        }
        catch (DO.DalUnauthorizedOperationException ex)
        {
            throw new BO.BlUnauthorizedOperationException("שגיאה בעדכון נתוני מתנדבים.");
        }
    }

}
