
using BlApi;
using BO;
using DO;
using Helpers;
using System.Data;
using System;
using System.Xml.Linq;
using DalApi;
namespace BlImplementation;

internal class VolunteerImplementation : BlApi.IVolunteer
{
    #region Stage 5
    public void AddObserver(Action listObserver) =>
    VolunteerManager.Observers.AddListObserver(listObserver); //stage 5
    public void AddObserver(int id, Action observer) =>
    VolunteerManager.Observers.AddObserver(id, observer); //stage 5
    public void RemoveObserver(Action listObserver) =>
    VolunteerManager.Observers.RemoveListObserver(listObserver); //stage 5
    public void RemoveObserver(int id, Action observer) =>
    VolunteerManager.Observers.RemoveObserver(id, observer); //stage 5
    #endregion Stage 5

    private readonly DalApi.IDal Volunteer_dal = DalApi.Factory.Get;
    public BO.Role Login(int id, string password)
    {
        // Retrieve the volunteer by username
        var volunteer = Volunteer_dal.Volunteer.ReadAll().FirstOrDefault(v => v.Id == id);
        if (volunteer == null)
        {
            throw new UnauthorizedAccessException("Invalid id or password.");
        }

        // Verify the password
        if (!Tools.VerifyPassword(password, volunteer.Password))
        {
            throw new UnauthorizedAccessException("Invalid id or password.");
        }

        // Return the role if the password is correct
        return (BO.Role)volunteer.Role;
    }

    public void Create(BO.Volunteer boVolunteer)
    {
        try
        {
            (double lat,double lon)=VolunteerManager.ValidateVolunteer(boVolunteer);
            var user = Volunteer_dal.Volunteer.ReadAll()
                .FirstOrDefault(u => u.Id == boVolunteer.Id);
            if (user != null)
                throw new BO.BlArgumentException("A volunteer with the same ID already exists. Please use a different ID.");
            string hashedPassword = Tools.HashPassword(boVolunteer.Password);
            var doVolunteer = new DO.Volunteer
            {
                Id = boVolunteer.Id,
                Name = boVolunteer.Name,
                Phone = boVolunteer.PhoneNumber,
                Email = boVolunteer.Email,
                //Password = boVolunteer.Password,
                Password = hashedPassword,
                Address = boVolunteer.CurrentAddress,
                latitude = lat,
                longitude = lon,
                Role = (DO.Role)boVolunteer.Role,
                Active = boVolunteer.Active,
                MaxDistance = boVolunteer.MaxDistance,
                TypeOfDistance = (DO.TypeOfDistance)boVolunteer.TypeOfDistance
            };

            Volunteer_dal.Volunteer.Create(doVolunteer);
            VolunteerManager.Observers.NotifyItemUpdated(doVolunteer.Id);  //stage 5
            VolunteerManager.Observers.NotifyListUpdated();  //stage 5

        }
        catch (DO.DalUnauthorizedOperationException ex)
        {
            throw new BO.BlUnauthorizedOperationException("users data access error.");
        }
    }

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
        VolunteerManager.Observers.NotifyItemUpdated(id);  //stage 5
        VolunteerManager.Observers.NotifyListUpdated();  //stage 5
    }


    public BO.Volunteer? Read(int id)
    {
        try
        {
            var doVolunteer = Volunteer_dal.Volunteer.Read(id)
                ?? throw new BO.BlDoesNotExistException("Volunteer not found.");

            var assignments = Volunteer_dal.Assignment.ReadAll().Where(a => a.VolunteerId == id).ToList();
            var assignment = assignments
                .FirstOrDefault(a => a.VolunteerId == id && a.TreatmentEndTime == null);

            BO.CallInProgress? callInProgress = null;

            if (assignment != null)
            {
                var call = Volunteer_dal.Call.Read(assignment.CallId);
                callInProgress = new BO.CallInProgress
                {
                    CallId = call.Id,
                    TypeOfCall = (BO.TypeOfCall)call.TypeOfCall,
                    Description = call.Description,
                    Address = call.Address,
                    TimeOfOpening = assignment.TreatmentStartTime,
                    MaxFinishTime = call.MaxClosingTime,
                    TimeOfEntryToTreatment = assignment.TreatmentStartTime,
                    CallVolunteerDistance =CallManager.GetAerialDistance(doVolunteer.Address,call.Address),
                    Status = BO.Status.BEING_HANDELED
                };
            }
            int numOfCompletedcalls=assignments.Where(assignment=>(BO.AssignmentStatus)assignment.AssignmentStatus==BO.AssignmentStatus.COMPLETED).Count();
            int numOfSelfcanceledcalls = assignments.Where(assignment => (DO.TypeOfTreatmentEnding)assignment.TypeOfTreatmentEnding == DO.TypeOfTreatmentEnding.SELF_CANCELED).Count();
            int numOfoutOfDatecalls = assignments.Where(assignment => (DO.TypeOfTreatmentEnding)assignment.TypeOfTreatmentEnding == DO.TypeOfTreatmentEnding.EXPIRED_CANCELED).Count();

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
                numOfCompletedcalls,
                numOfSelfcanceledcalls,
                numOfoutOfDatecalls,
               callInProgress);
        }
        catch (DO.DalUnauthorizedOperationException ex)
        {
            throw new BO.BlUnauthorizedOperationException("Error accessing volunteer details.");
        }
    }

    public IEnumerable<BO.VolunteerInList> ReadAll(BO.Active? filter = null, BO.VolunteerFields? sort = null, BO.TypeOfCall? typeOfCallFilter = null)
    {
        try
        {
            var volunteers = Volunteer_dal.Volunteer.ReadAll();
            var assignments = Volunteer_dal.Assignment.ReadAll();

            // סינון לפי סטטוס
            if (filter.HasValue)
                volunteers = volunteers.Where(v => v.Active == (filter == BO.Active.TRUE)).ToList();

            var volunteerList = volunteers.Select(v =>
            {
                var assignmentForVolunteer = assignments.FirstOrDefault(a => a.VolunteerId == v.Id && a.TreatmentEndTime == null);
                var callId = assignmentForVolunteer?.CallId;

                return new BO.VolunteerInList
                {
                    Id = v.Id,
                    Name = v.Name,
                    Active = v.Active,
                    CallsDone = assignments.Count(assignment => assignment.VolunteerId == v.Id && (BO.AssignmentStatus)assignment.AssignmentStatus == BO.AssignmentStatus.COMPLETED),
                    CallsCanceled = assignments.Count(assignment => assignment.VolunteerId == v.Id && (DO.TypeOfTreatmentEnding)assignment.TypeOfTreatmentEnding == DO.TypeOfTreatmentEnding.SELF_CANCELED),
                    CallsOutOfDate = assignments.Count(assignment => assignment.VolunteerId == v.Id && (DO.TypeOfTreatmentEnding)assignment.TypeOfTreatmentEnding == DO.TypeOfTreatmentEnding.EXPIRED_CANCELED),
                    CallId = callId,
                    TypeOfCall = callId != null ? (BO.TypeOfCall)Volunteer_dal.Call.Read(callId.Value).TypeOfCall : BO.TypeOfCall.NONE,
                };
            });
            if (typeOfCallFilter.HasValue)
                volunteerList = volunteerList.Where(v => v.TypeOfCall == typeOfCallFilter).ToList();

            // מיון לפי שדה ספציפי
            if (sort.HasValue && sort!=BO.VolunteerFields.None)
            {
                volunteerList = sort switch
                {
                    BO.VolunteerFields.Name => volunteerList.OrderBy(v => v.Name),
                    BO.VolunteerFields.Id => volunteerList.OrderBy(v => v.Id),
                    BO.VolunteerFields.CallsDone => volunteerList.OrderBy(v => v.CallsDone),
                    BO.VolunteerFields.CallsCanceled => volunteerList.OrderBy(v => v.CallsCanceled),
                    BO.VolunteerFields.CallsOutOfdate => volunteerList.OrderBy(v => v.CallsOutOfDate),
                    BO.VolunteerFields.Active => volunteerList.OrderByDescending(v => v.Active),
                    _ => volunteerList.OrderBy(v => v.Id)
                };
            }

            return volunteerList.ToList();
        }
        catch (DO.DalUnauthorizedOperationException ex)
        {
            throw new BO.BlUnauthorizedOperationException("Error accessing volunteer details.");
        }
    }


    public void Update(int userId ,BO.Volunteer boVolunteer)
    {
        try { 
             (double? lat, double? lon) = VolunteerManager.ValidateVolunteer(boVolunteer);
             var user = Volunteer_dal.Volunteer.ReadAll()
                 .FirstOrDefault(u => u.Id == boVolunteer.Id);
            //בשלב 5 צריך להוסיף שאם התז הוא לא של המשתמש הנוכחי או של המנהל אז לא ניתן לעדכן את המתנדב
            if (user == null )
                  throw new BO.BlArgumentException("username or password are incorrect.");
             var doVolunteer = new DO.Volunteer
             {
                 Id = boVolunteer.Id,
                 Name = boVolunteer.Name,
                 Phone = boVolunteer.PhoneNumber,
                 Email = boVolunteer.Email,
                 Password = Tools.HashPassword(boVolunteer.Password),
                 Address = boVolunteer.CurrentAddress,
                 latitude = lat,
                 longitude = lon,
                 Role = (DO.Role)boVolunteer.Role,
                 Active = boVolunteer.Active,
                 MaxDistance = boVolunteer.MaxDistance,
                 TypeOfDistance = (DO.TypeOfDistance)boVolunteer.TypeOfDistance
             };

             Volunteer_dal.Volunteer.Update(doVolunteer);
            VolunteerManager.Observers.NotifyItemUpdated(boVolunteer.Id);  //stage 5
            VolunteerManager.Observers.NotifyListUpdated();  //stage 5
        }
        catch (DO.DalUnauthorizedOperationException ex)
        {
            throw new BO.BlUnauthorizedOperationException("users data access error.");
        }
    }

}
