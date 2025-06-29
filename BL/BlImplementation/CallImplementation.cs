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
            if (Call_dal.Volunteer.Read(volunteerId) is not { Active: true })
                throw new BO.BlDoesNotExistException("The volunteer is not active in the system.");

            // Validate call exists
            var call = Call_dal.Call.Read(callId)
                ?? throw new BO.BlDoesNotExistException("call is not found in the system.");


            // Get all assignments for this call (past and present)
            var callAssignments = Call_dal.Assignment
                .ReadAll(a => a.CallId == callId).ToList();
            var callStatus=CallManager.CalculateCallStatus(callAssignments, call.MaxClosingTime);

            // Validate call status
            if ((DO.CallStatus)callStatus != DO.CallStatus.OPEN_IN_RISK && (DO.CallStatus)callStatus != DO.CallStatus.OPEN)
                throw new BO.BlDoesNotExistException("this call is not open for treatment.");

            bool isBeingTreated = callAssignments.Any(a =>
                //a.CallId == callId &&
                a.AssignmentStatus != DO.AssignmentStatus.OPEN &&
                a.AssignmentStatus != DO.AssignmentStatus.COMPLETED);

            // Check if the call is already being treated
            if (isBeingTreated)
                throw new BO.BlDoesNotExistException("this call is already being treated.");

            // Create new assignment
            var newAssignment = new DO.Assignment
            {
                VolunteerId = volunteerId,
                CallId = callId,
                AssignmentStatus = DO.AssignmentStatus.OPEN,
                TreatmentStartTime = AdminManager.Now,
                TreatmentEndTime = null,
            };
            
            //callAssignments.Add(new BO.CallAssignInList
            //{
            //    VolunteerId = volunteerId,
            //    Name = Call_dal.Volunteer.Read(volunteerId)?.Name,
            //    TimeOfStarting = newAssignment.TreatmentStartTime,
            //    TimeOfEnding = null,
            //    TypeOfTreatmentEnding = null,
            //});
            // Create the assignment and update call status
            Call_dal.Assignment.Create(newAssignment);
            //Update(new BO.Call(
            //    call.Id,
            //    (BO.TypeOfCall)call.TypeOfCall,
            //    call.Description,
            //    call.Address,
            //    call.latitude,
            //    call.longitude,
            //    call.OpeningTime,
            //    call.MaxClosingTime,
            //    BO.CallStatus.BEING_HANDELED,
            //    callAssignments
            //));
        }

        public void Create(BO.Call boCall)
        {
            CallManager.ValidateCall(boCall);
            (double? lat, double? lon) = CallManager.GetCoordinates(boCall.Address!);
            var doCall = new DO.Call(boCall.Id, (DO.TypeOfCall)boCall.TypeOfCall, boCall.Description, boCall.Address!, lat, lon, null, boCall.OpeningTime, DO.CallStatus.OPEN, boCall.MaxClosingTime);
            Call_dal.Call.Create(doCall);
            CallManager.SendEmailWhenCallOpened(boCall);
            CallManager.Observers.NotifyItemUpdated(doCall.Id);  //stage 5
            CallManager.Observers.NotifyListUpdated();  //stage 5
        }

        public void Delete(int id)
        {
            var call = Call_dal.Call.Read(id)
                ?? throw new BO.BlDoesNotExistException("Call not found in the system.");

            if (call.Status == DO.CallStatus.CLOSED || Call_dal.Assignment.ReadAll().Any(a => a.CallId == id))
                throw new BO.BlDeletionImpossible("It is not possible to cancle this call.");

            Call_dal.Call.Delete(id);
            CallManager.Observers.NotifyItemUpdated(id);  //stage 5
            CallManager.Observers.NotifyListUpdated();  //stage 5

        }

        public BO.Call GetCallDetails(int id)
        {
            var doCall = Call_dal.Call.Read(id)
                ?? throw new BO.BlDoesNotExistException("Call not found in the system.");
            var assignments = Call_dal.Assignment.ReadAll();
            var assignmentsList = assignments
                .Where(a => a.CallId == id)
                .Select(a => new BO.CallAssignInList
                {
                    VolunteerId = a.VolunteerId,
                    Name = Call_dal.Volunteer.Read(a.VolunteerId)?.Name,
                    TimeOfStarting = a.TreatmentStartTime,
                    TimeOfEnding = a.TreatmentEndTime,
                    TypeOfTreatmentEnding = (BO.TypeOfTreatmentEnding)a.TypeOfTreatmentEnding!,
                }).ToList();
            BO.CallStatus status = CallManager.CalculateCallStatus(assignments, doCall.MaxClosingTime);
            return new BO.Call(doCall.Id, (BO.TypeOfCall)doCall.TypeOfCall, doCall.Description, doCall.Address, doCall.latitude, doCall.longitude, doCall.OpeningTime, doCall.MaxClosingTime, status, assignmentsList);
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
            // Step 1: Retrieve all calls and assignments
            var calls = Call_dal.Call.ReadAll();
            var assignments = Call_dal.Assignment.ReadAll();

            // Step 2: Calculate the status and other details for each call
            var callList = calls.Select(c =>
            {
                var callAssignments = assignments.Where(a => a.CallId == c.Id).ToList();
                var lastAssignment = callAssignments.OrderBy(a => a.TreatmentStartTime).LastOrDefault();
                var lastVolunteerName = lastAssignment != null
                    ? Call_dal.Volunteer.Read(lastAssignment.VolunteerId)?.Name
                    : null;

                return new BO.CallInList
                {
                    Id = lastAssignment?.VolunteerId,
                    CallId = c.Id,
                    TypeOfCall = (BO.TypeOfCall)c.TypeOfCall,
                    OpeningTime = c.OpeningTime,
                    TimeLeft = c.MaxClosingTime != null ? (c.MaxClosingTime - AdminManager.Now).Value : TimeSpan.Zero,
                    LastVolunteerName = lastVolunteerName,
                    TreatmentDuration = lastAssignment != null ? (lastAssignment.TreatmentEndTime - lastAssignment.TreatmentStartTime) : null,
                    Status = CallManager.CalculateCallStatus(callAssignments, c.MaxClosingTime),
                    AssignmentsSum = callAssignments.Count()
                };
            }).ToList();

            // Step 3: Apply filtering
            if (filterBy != null && filter != null)
            {
                callList = callList.Where(c => CallManager.MatchField(filterBy, c, filter)).ToList();
            }

            // Step 4: Apply sorting
            if (sortBy != null)
            {
                callList = CallManager.SortByField(sortBy, callList).ToList();
            }

            return callList;
        }



        //public IEnumerable<BO.ClosedCallInList> GetClosedCallsHandledByTheVolunteer(int volunteerId, Enum? filterBy, object? filterValue, Enum? sortBy)
        //{
        //    var calls = Call_dal.Assignment.ReadAll()
        //        .Where(a => a.VolunteerId == volunteerId && a.AssignmentStatus == DO.AssignmentStatus.CLOSED)
        //        .Select(a => a.CallId)
        //        .Distinct()
        //        .Select(id => Call_dal.Call.Read(id))
        //        .Where(c => c.Status == DO.CallStatus.CLOSED);

        //    // Apply filtering using the new filterValue parameter
        //    if (filterBy != null && filterValue != null)
        //    {
        //        calls = calls.Where(c => CallManager.MatchField(filterBy, c, filterValue));
        //    }

        //    var closedCalls = calls.Select(c => new BO.ClosedCallInList
        //    {
        //        Id = c.Id,
        //        TypeOfCall = (BO.TypeOfCall)c.TypeOfCall,
        //        OpeningTime = c.OpeningTime,
        //        Address = c.Address,
        //    });

        //    if (sortBy != null)
        //    {
        //        closedCalls = CallManager.SortByField(sortBy, closedCalls);
        //    }

        //    return closedCalls.ToList();
        //}

        public IEnumerable<BO.ClosedCallInList> GetClosedCallsHandledByTheVolunteer(int volunteerId, Enum? filterBy, object? filterValue, Enum? sortBy)
        {
            // Step 1: Retrieve all assignments for the volunteer with CLOSED status
            var assignments = Call_dal.Assignment.ReadAll()
                .Where(a => a.VolunteerId == volunteerId && a.AssignmentStatus == DO.AssignmentStatus.CLOSED);

            // Step 2: Retrieve the corresponding calls
            var calls = assignments
                .Select(a => new
                {
                    Assignment = a,
                    Call = Call_dal.Call.Read(a.CallId)
                })
                .Where(x => x.Call != null && x.Call.Status == DO.CallStatus.CLOSED); // Add null check and status filter

            // Step 3: Apply filtering
            if (filterBy != null && filterValue != null)
            {
                calls = calls.Where(x => CallManager.MatchField(filterBy, x.Call!, filterValue));
            }

            // Step 4: Map to BO.ClosedCallInList
            var closedCalls = calls.Select(x => new BO.ClosedCallInList
            {
                Id = x.Call!.Id, // Call ID
                TypeOfCall = (BO.TypeOfCall)x.Call.TypeOfCall,
                Address = x.Call.Address,
                OpeningTime = x.Call.OpeningTime,
                ActualTreatmentEndTime = x.Assignment.TreatmentEndTime, // From the assignment
                TypeOfTreatmentEnding = (BO.TypeOfTreatmentEnding?)x.Assignment.TypeOfTreatmentEnding // From the assignment
            });

            // Step 5: Apply sorting
            if (sortBy != null)
            {
                closedCalls = CallManager.SortByField(sortBy, closedCalls);
            }

            return closedCalls.ToList();
        }

        public IEnumerable<BO.OpenCallInList> GetOpenCallsCanBeSelectedByAVolunteer(int volunteerId, Enum? filterBy, Enum? sortBy)
        {
            if (Call_dal.Volunteer.Read(volunteerId) is not { Active: true })
                throw new BO.BlDoesNotExistException("The volunteer is not active in the system.");

            var calls = Call_dal.Call.ReadAll();
            var assignments = Call_dal.Assignment.ReadAll();
            var assignmentsGrouped = assignments.GroupBy(a => a.CallId);

            var openOrRiskyCalls = calls
                .Select(call =>
                {
                    var relatedAssignments = assignmentsGrouped.FirstOrDefault(g => g.Key == call.Id)?.ToList();
                    CallManager.CalculateCallStatus(relatedAssignments ?? Enumerable.Empty<Assignment>(), call.MaxClosingTime);
                    //CallManager.CalculateCallStatus(relatedAssignments, call.MaxClosingTime);
                    return call;
                })
                .Where(call => call.Status == DO.CallStatus.OPEN || call.Status == DO.CallStatus.OPEN_IN_RISK) // Adjust status properties if needed
                .ToList();

            if (filterBy != null)
            {
                calls = calls.Where(c => CallManager.MatchField(filterBy, c, null));
            }

            var openCalls = calls.Select(c => new BO.OpenCallInList
            {
                Id = c.Id,
                TypeOfCall = (BO.TypeOfCall)c.TypeOfCall,
                Description = c.Description,
                Address = c.Address,
                OpeningTime = c.OpeningTime,
                MaxCloseingTime = c.MaxClosingTime,
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
                ?? throw new BO.BlDoesNotExistException("Assignment not found.");

            if (assignment.AssignmentStatus == DO.AssignmentStatus.COMPLETED|| assignment.AssignmentStatus == DO.AssignmentStatus.OUT_OF_DATE)
                throw new BO.BlUnauthorizedOperationException("It is not possible to cancle an assignment that is completed or out of date.");

            var TypeOfTreatmentEnding = assignment.VolunteerId == volunteerId ?
                DO.TypeOfTreatmentEnding.SELF_CANCELED:
                Call_dal.Volunteer.Read(volunteerId) == null ? throw new BO.BlDoesNotExistException("Only administrator or oawner of assignment can cancle it.") :
                 DO.TypeOfTreatmentEnding.MANAGER_CANCELED;

            var newAssignment = new DO.Assignment
            {
                Id = assignment.Id,
                CallId = assignment.CallId,
                VolunteerId = assignment.VolunteerId,
                TreatmentStartTime = assignment.TreatmentStartTime,
                TreatmentEndTime = AdminManager.Now,
                TypeOfTreatmentEnding = TypeOfTreatmentEnding,
                AssignmentStatus = DO.AssignmentStatus.CLOSED
            };
            Call_dal.Assignment.Update(newAssignment);
            var call = GetCallDetails(assignment.CallId)
                ?? throw new BO.BlDoesNotExistException("Call not found in the system.");
           var status = CallManager.CalculateCallStatus(Call_dal.Assignment.ReadAll(), call.MaxClosingTime);
            call.AssignedVolunteers!.Remove(call.AssignedVolunteers.FirstOrDefault(a => a.VolunteerId == assignment.VolunteerId));
           Update(new BO.Call(call.Id, call.TypeOfCall, call.Description, call.Address, call.Latitude, call.Longitude, call.OpeningTime, call.MaxClosingTime, status,call.AssignedVolunteers));

        }

        public void TreatmentCompletionUpdate(int volunteerId, int assignmentId)
        {
            var assignment = Call_dal.Assignment.Read(assignmentId)
                ?? throw new BO.BlDoesNotExistException("הקצאה לא נמצאה במערכת.");

            if (assignment.AssignmentStatus != DO.AssignmentStatus.OPEN)
                throw new BO.BlUnauthorizedOperationException("לא ניתן לעדכן סיום לטיפול שכבר טופל או שפג תוקפו.");

            if (assignment.VolunteerId != volunteerId)
                throw new BO.BlUnauthorizedOperationException("רק המתנדב שהוקצה יכול לעדכן סיום טיפול.");
            Call_dal.Assignment.Update(new DO.Assignment(assignment.Id, assignment.CallId, assignment.VolunteerId, assignment.TreatmentStartTime, AdminManager.Now, DO.TypeOfTreatmentEnding.HANDLED, DO.AssignmentStatus.COMPLETED));
            var call = GetCallDetails(assignment.CallId)
                ?? throw new BO.BlDoesNotExistException("קריאה לא נמצאה במערכת.");
            call.AssignedVolunteers!.Remove(call.AssignedVolunteers.FirstOrDefault(a => a.VolunteerId == volunteerId));
            Update(new BO.Call(call.Id, call.TypeOfCall, call.Description, call.Address, call.Latitude, call.Longitude, call.OpeningTime, call.MaxClosingTime, BO.CallStatus.CLOSED, call.AssignedVolunteers));
        }

        public void Update(BO.Call boCall)
        {
            CallManager.ValidateCall(boCall);
            DO.Call doCall = Call_dal.Call.Read(boCall.Id)
                ?? throw new BO.BlDoesNotExistException("קריאה לא נמצאה במערכת.");
            Call_dal.Call.Update(new DO.Call(boCall.Id,
            (DO.TypeOfCall)boCall.TypeOfCall,
            boCall.Description,
            boCall.Address!,
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
