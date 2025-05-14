using BlApi;
using BO;
using DalApi;
using DO;
using Helpers;
using System.Collections;
using System.Net;

namespace BlImplementation
{
    internal class CallImplementation : BlApi.ICall
    {
        #region Stage 5
        public void AddObserver(Action listObserver) =>
        CallManager.Observers.AddListObserver(listObserver); //stage 5
        public void AddObserver(int id, Action observer) =>
        CallManager.Observers.AddObserver(id, observer); //stage 5
        public void RemoveObserver(Action listObserver) =>
        CallManager.Observers.RemoveListObserver(listObserver); //stage 5
        public void RemoveObserver(int id, Action observer) =>
        CallManager.Observers.RemoveObserver(id, observer); //stage 5
        #endregion Stage 5

        private readonly DalApi.IDal Call_dal = DalApi.Factory.Get;
        public void ChoosingACallForTreatment(int volunteerId, int callId)
        {
            // Validate call exists
            var call = Call_dal.Call.Read(callId)
                ?? throw new BO.BlDoesNotExistException("call is not found in the system.");

            // Validate call status
            if (call.Status == DO.CallStatus.BEING_HANDELED || call.Status == DO.CallStatus.BEING_HANDELED_IN_RISK)
                throw new BO.BlDoesNotExistException("this call is not open for treatment.");

            // Get all assignments for this call (past and present)
            List<BO.CallAssignInList>? callAssignments = Call_dal.Assignment
                .ReadAll()
                .Where(a => a.CallId == callId)
                .Select(a => new BO.CallAssignInList
                {
                    VolunteerId = a.VolunteerId,
                    // Add other necessary properties
                })
                .ToList();

            // Check if call is already being treated
            if (callAssignments.Any())
                throw new BO.BlDoesNotExistException("this call is already being treated.");

            // Create new assignment
            var newAssignment = new DO.Assignment
            {
                VolunteerId = volunteerId,
                CallId = callId,
                TreatmentStartTime = AdminManager.Now,
                TreatmentEndTime = null,
            };

            // Create the assignment and update call status
            Call_dal.Assignment.Create(newAssignment);
            Update(new BO.Call(
                call.Id,
                (BO.TypeOfCall)call.TypeOfCall,
                call.Description,
                call.Address,
                call.latitude,
                call.longitude,
                call.OpeningTime,
                call.MaxClosingTime,
                BO.CallStatus.BEING_HANDELED,
                callAssignments
            ));
        }

        public void Create(BO.Call boCall)
        {
            CallManager.ValidateCall(boCall);
            (double? lat, double? lon) = CallManager.GetCoordinates(boCall.Address);
            var doCall = new DO.Call(boCall.Id, (DO.TypeOfCall)boCall.TypeOfCall, boCall.Description, boCall.Address, lat, lon, null, boCall.OpeningTime, DO.CallStatus.OPEN, boCall.MaxClosingTime);
            Call_dal.Call.Create(doCall);
            CallManager.SendEmailWhenCallOpened(boCall);
            CallManager.Observers.NotifyItemUpdated(doCall.Id);  //stage 5
            CallManager.Observers.NotifyListUpdated();  //stage 5
        }

        public void Delete(int id)
        {
            var call = Call_dal.Call.Read(id)
                ?? throw new BO.BlDoesNotExistException("Call not found in the system.");

            if (call.Status != DO.CallStatus.CLOSED || Call_dal.Assignment.ReadAll().Any(a => a.CallId == id))
                throw new BO.BlDeletionImpossible("It is not possible to cancle this call.");

            Call_dal.Call.Delete(id);
            CallManager.Observers.NotifyItemUpdated(id);  //stage 5
            CallManager.Observers.NotifyListUpdated();  //stage 5

        }

        public BO.Call GetCallDetails(int id)
        {
            var doCall = Call_dal.Call.Read(id)
                ?? throw new BO.BlDoesNotExistException("Call not found in the system.");

            var assignments = Call_dal.Assignment.ReadAll()
                .Where(a => a.CallId == id)
                .Select(a => new BO.CallAssignInList
                {
                    VolunteerId = a.VolunteerId,
                    TimeOfStarting = a.TreatmentStartTime,
                    TimeOfEnding = a.TreatmentEndTime,
                    //Status = (BO.Status)a.Status
                }).ToList();

            return new BO.Call(doCall.Id, (BO.TypeOfCall)doCall.TypeOfCall, doCall.Description, doCall.Address, doCall.latitude, doCall.longitude, doCall.OpeningTime, doCall.MaxClosingTime, (BO.CallStatus)doCall.Status, assignments);

        }

        public IEnumerable<int> GetCallsCount()
        {
            return Call_dal.Call.ReadAll()
                .GroupBy(c => c.Status)
                .OrderBy(g => g.Key)
                .Select(g => g.Count())
                .ToArray();
        }

        public IEnumerable<BO.CallInList> ReadAll(Enum? filterBy, object? filter, Enum? sortBy)
        {
            var calls = Call_dal.Call.ReadAll();

            if (filterBy != null && filter != null)
            {
                calls = calls.Where(c => CallManager.MatchField(filterBy, c, filter));
            }

            //var callList = calls.Select(c => new BO.CallInList
            //{
            //    //Id = c.Id,
            //    CallId = c.Id,
            //    TypeOfCall = (BO.TypeOfCall)c.TypeOfCall,
            //    OpeningTime = c.OpeningTime,
            //    TimeLeft = c.MaxClosingTime != null ? (c.MaxClosingTime - AdminManager.Now).Value : TimeSpan.Zero,
            //    LastVolunteerName = c.AssignedVolunteers?.LastOrDefault()?.Name,                //Description = c.Description,
            //    Status = (BO.CallStatus)c.Status,
            //    //Address = c.Address
            //});
            var assignments = Call_dal.Assignment.ReadAll();
            var callList = from c in calls
                           join assignment in assignments on c.Id equals assignment.CallId into callAssignments
                           let lastAssignment = callAssignments.OrderBy(a => a.TreatmentStartTime).LastOrDefault()
                           let lastVolunteerName = lastAssignment != null
                               ? Call_dal.Volunteer.Read(lastAssignment.VolunteerId)?.Name
                               : null
                           select new BO.CallInList
                           {
                               CallId = c.Id,
                               TypeOfCall = (BO.TypeOfCall)c.TypeOfCall,
                               OpeningTime = c.OpeningTime,
                               TimeLeft = c.MaxClosingTime != null ? (c.MaxClosingTime - AdminManager.Now).Value : TimeSpan.Zero,
                               LastVolunteerName = lastVolunteerName,
                               TreatmentDuration = lastAssignment != null ? (lastAssignment.TreatmentEndTime - lastAssignment.TreatmentStartTime) : null,
                               Status = CallManager.CalculateCallStatus(callAssignments, c.MaxClosingTime),
                               AssignmentsSum = callAssignments.Count()
                           };

            if (sortBy != null)
            {
                callList = CallManager.SortByField(sortBy, callList);
            }

            return callList.ToList();
        }

        public IEnumerable<BO.ClosedCallInList> GetClosedCallsHandledByTheVolunteer(int volunteerId, Enum? filterBy, object? filterValue, Enum? sortBy)
        {
            var calls = Call_dal.Assignment.ReadAll()
                .Where(a => a.VolunteerId == volunteerId && a.AssignmentStatus == DO.AssignmentStatus.CLOSED)
                .Select(a => a.CallId)
                .Distinct()
                .Select(id => Call_dal.Call.Read(id))
                .Where(c => c.Status == DO.CallStatus.CLOSED);

            // Apply filtering using the new filterValue parameter
            if (filterBy != null && filterValue != null)
            {
                calls = calls.Where(c => CallManager.MatchField(filterBy, c, filterValue));
            }

            var closedCalls = calls.Select(c => new BO.ClosedCallInList
            {
                Id = c.Id,
                TypeOfCall = (BO.TypeOfCall)c.TypeOfCall,
                OpeningTime = c.OpeningTime,
                Address = c.Address,
            });

            if (sortBy != null)
            {
                closedCalls = CallManager.SortByField(sortBy, closedCalls);
            }

            return closedCalls.ToList();
        }


        public IEnumerable<BO.OpenCallInList> GetOpenCallsCanBeSelectedByAVolunteer(int volunteerId, Enum? filterBy, Enum? sortBy)
        {
            var calls = Call_dal.Call.ReadAll()
                .Where(c => c.Status == DO.CallStatus.OPEN || c.Status == DO.CallStatus.OPEN_IN_RISK);

            if (filterBy != null)
            {
                calls = calls.Where(c => CallManager.MatchField(filterBy, c, null));
            }

            var openCalls = calls.Select(c => new BO.OpenCallInList
            {
                Id = c.Id,
                Address = c.Address,
                CallVolunteerDistance = CallManager.GetAerialDistance(Call_dal.Volunteer.Read(volunteerId).Address, c.Address)
            });

            if (sortBy != null)
            {
                openCalls = CallManager.SortByField(sortBy, openCalls);
            }

            return openCalls.ToList();
        }

        public void TreatmentCancellationUpdate(int volunteerId, int assignmentId)
        {
            var assignment = Call_dal.Assignment.Read(assignmentId)
                ?? throw new BO.BlDoesNotExistException("הקצאה לא נמצאה במערכת.");

            //if (assignment.Status != DO.AssignmentStatus.Open)
            //    throw new BO.InvalidOperationException("לא ניתן לבטל הקצאה שכבר טופלה או שפג תוקפה.");

            //assignment.Status = assignment.VolunteerId == volunteerId ?
            //    DO.AssignmentStatus.CanceledByVolunteer :
            //    DO.AssignmentStatus.CanceledByManager;

            var newAssignment = new DO.Assignment
            {
                Id = assignment.Id,
                TreatmentEndTime = AdminManager.Now
            };
            Call_dal.Assignment.Update(newAssignment);
        }

        public void TreatmentCompletionUpdate(int volunteerId, int assignmentId)
        {
            var assignment = Call_dal.Assignment.Read(assignmentId)
                ?? throw new BO.BlDoesNotExistException("הקצאה לא נמצאה במערכת.");

            if (assignment.AssignmentStatus != DO.AssignmentStatus.OPEN)
                throw new BO.BlUnauthorizedOperationException("לא ניתן לעדכן סיום לטיפול שכבר טופל או שפג תוקפו.");

            if (assignment.VolunteerId != volunteerId)
                throw new BO.BlUnauthorizedOperationException("רק המתנדב שהוקצה יכול לעדכן סיום טיפול.");
            Console.WriteLine("Enter the treatment end type(HOSPITAL_ADMISSION=0, STAY_AT_HOME,DEAD=1 , EXPIRED=2, UNMATCHED=3):");
            DO.TypeOfTreatmentEnding typeOfTreatmentEnding = (DO.TypeOfTreatmentEnding)Enum.Parse(typeof(DO.TypeOfTreatmentEnding), Console.ReadLine()!);

            Call_dal.Assignment.Update(new DO.Assignment(assignment.Id, assignment.CallId, assignment.VolunteerId, assignment.TreatmentStartTime, AdminManager.Now, typeOfTreatmentEnding, null));
        }

        public void Update(BO.Call boCall)
        {
            CallManager.ValidateCall(boCall);
            DO.Call doCall = Call_dal.Call.Read(boCall.Id)
                ?? throw new BO.BlDoesNotExistException("קריאה לא נמצאה במערכת.");
            Call_dal.Call.Update(new DO.Call(boCall.Id,
            (DO.TypeOfCall)boCall.TypeOfCall,
            boCall.Description,
            boCall.Address,
            doCall.latitude,
            doCall.longitude,
            doCall.riskRange,
            doCall.OpeningTime,
            (DO.CallStatus)boCall.Status,
            boCall.MaxClosingTime));
            CallManager.Observers.NotifyItemUpdated(doCall.Id);  //stage 5
            CallManager.Observers.NotifyListUpdated();  //stage 5
        }
    }
}
