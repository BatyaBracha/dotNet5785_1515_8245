using BlApi;
using BO;
using DO;
using Helpers;
using System.Net;

namespace BlImplementation
{
    internal class CallImplementation : ICall
    {
        private readonly DalApi.IDal Call_dal = DalApi.Factory.Get;
        private void ValidateCall(BO.Call boCall)
        {
            if (boCall == null)
                throw new BlNullPropertyException( "The call object cannot be null.",null);

            // Check if the ID is valid
            if (boCall.Id <= 0)
                throw new BlArgumentException("Call ID must be a positive number.");

            // Validate time constraints
            if (boCall.MaxClosingTime.HasValue && boCall.MaxClosingTime.Value <= boCall.OpeningTime)
                throw new BlArgumentException("End time must be greater than the start time.");

            // Check if the address is valid
            if (string.IsNullOrWhiteSpace(boCall.Address))
                throw new BlArgumentException("Address cannot be null or empty.");

            // Validate the address against a geolocation service
            (double? lat, double? lon) = CallManager.GetCoordinates(boCall.Address);

            if (lat==null || lon == null)
                throw new BlArgumentException("Address is invalid or could not be found.");

            // Assign valid coordinates
            boCall.Latitude =lat;
            boCall.Longitude = lon;

            // Check for other business logic conditions if needed
            if (boCall.Status == BO.CallStatus.CLOSED && boCall.OpeningTime > DateTime.Now)
                throw new BlArgumentException("A closed call cannot have a start time in the future.");
        }
        public void ChoosingACallForTreatment(int volunteerId, int callId)
        {
            var call = Call_dal.Call.Read(callId)
                ?? throw new BO.BlDoesNotExistException("קריאה לא נמצאה במערכת.");

            if (call.Status != DO.CallStatus.BEING_HANDELED && call.Status != DO.CallStatus.BEING_HANDELED_IN_RISK)
                throw new BO.BlDoesNotExistException("קריאה זו אינה פתוחה לטיפול.");

            if (Call_dal.Assignment.ReadAll().Any(a => a.CallId == callId))
                throw new BO.BlDoesNotExistException("קריאה זו כבר בטיפול.");

            var newAssignment = new DO.Assignment
            {
                VolunteerId = volunteerId,
                CallId = callId,
                TreatmentStartTime = ClockManager.Now,
                TreatmentEndTime = null,
            };

            Call_dal.Assignment.Create(newAssignment);
        }

        public void Create(BO.Call boCall)
        {
            ValidateCall(boCall);

            var doCall = new DO.Call
            {
                Id = boCall.Id,
                Description = boCall.Description,
                Address = boCall.Address,
                latitude = boCall.Latitude,
                longitude = boCall.Longitude,
                OpeningTime = boCall.OpeningTime,
                MaxClosingTime = boCall.MaxClosingTime
            };

            Call_dal.Call.Create(doCall);
        }

        public void Delete(int id)
        {
            var call = Call_dal.Call.Read(id)
                ?? throw new BO.BlDoesNotExistException("קריאה לא נמצאה במערכת.");

            if (call.Status != DO.CallStatus.BEING_HANDELED || Call_dal.Assignment.ReadAll().Any(a => a.CallId == id))
                throw new BO.BlDeletionImpossible("לא ניתן למחוק קריאה זו.");

            Call_dal.Call.Delete(id);
        }

        public BO.Call GetCallDetails(int id)
        {
            var doCall = Call_dal.Call.Read(id)
                ?? throw new BO.BlDoesNotExistException("קריאה לא נמצאה במערכת.");

            var assignments = Call_dal.Assignment.ReadAll()
                .Where(a => a.CallId == id)
                .Select(a => new BO.CallAssignInList
                {
                    VolunteerId = a.VolunteerId,
                    TimeOfStarting = a.TreatmentStartTime,
                    TimeOfEnding = a.TreatmentEndTime,
                    //Status = (BO.Status)a.Status
                }).ToList();

            return new BO.Call
            {
                Id = doCall.Id,
                Description = doCall.Description,
                Status = (BO.CallStatus)doCall.Status,
                Address = doCall.Address,
                Latitude = doCall.latitude,
                Longitude = doCall.longitude,
                OpeningTime = doCall.OpeningTime,
                MaxClosingTime = doCall.MaxClosingTime,
                AssignedVolunteers = assignments
            };
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
                calls = calls.Where(c => MatchField(filterBy, c, filter));
            }

            var callList = calls.Select(c => new BO.CallInList
            {
                Id = c.Id,
                //Description = c.Description,
                Status = (BO.CallStatus)c.Status,
                //Address = c.Address
            });

            if (sortBy != null)
            {
                callList = SortByField(sortBy, callList);
            }

            return callList.ToList();
        }

        public IEnumerable<BO.ClosedCallInList> GetClosedCallsHandledByTheVolunteer(int volunteerId, Enum? sortBy)
        {
            var calls = Call_dal.Assignment.ReadAll()
                .Where(a => a.VolunteerId == volunteerId && a.AssignmentStatus == DO.AssignmentStatus.CLOSED)
                .Select(a => a.CallId)
                .Distinct()
                .Select(id => Call_dal.Call.Read(id))
                .Where(c => c.Status == DO.CallStatus.CLOSED);

            var closedCalls = calls.Select(c => new BO.ClosedCallInList
            {
                Id = c.Id,
                //Name = c.Name,
                Address = c.Address
            });

            if (sortBy != null)
            {
                closedCalls = SortByField(sortBy, closedCalls);
            }

            return closedCalls.ToList();
        }

        private bool MatchField(Enum? filterBy, DO.Call call, object? filterValue)
        {
            if (filterBy == null)
                return true;

            // Match by the field defined in the filterBy enum
            switch (filterBy)
            {
                case BO.CallField.STATUS:
                    return call.Status.Equals(filterValue);
                case BO.CallField.PRIORITY:
                    return call.riskRange.Equals(filterValue);
                case BO.CallField.TYPE:
                    return call.TypeOfCall.Equals(filterValue);
                default:
                    throw new BlNullPropertyException("Unsupported filter field", nameof(filterBy));
            }
        }

        private IEnumerable<BO.OpenCallInList> SortByField(Enum? sortBy, IEnumerable<BO.OpenCallInList> openCalls)
        {
            if (sortBy == null)
                return openCalls.OrderBy(c => c.Id); // Default sort by ID

            // Sort by the field defined in the sortBy enum
            return sortBy switch
            {
                BO.CallField.ADDRESS => openCalls.OrderBy(c => c.Address),
                BO.CallField.CALL_VOLUNTEER_DISTANCE => openCalls.OrderBy(c => c.CallVolunteerDistance),
                BO.CallField.ID => openCalls.OrderBy(c => c.Id),
                _ => throw new BlNullPropertyException("Unsupported sort field", nameof(sortBy)),
            };
        }

        public IEnumerable<BO.OpenCallInList> GetOpenCallsCanBeSelectedByAVolunteer(int volunteerId, Enum? filterBy, Enum? sortBy)
        {
            var calls = Call_dal.Call.ReadAll()
                .Where(c => c.Status == DO.CallStatus.OPEN || c.Status == DO.CallStatus.OPEN_IN_RISK);

            if (filterBy != null)
            {
                calls = calls.Where(c => MatchField(filterBy, c, null));
            }

            var openCalls = calls.Select(c => new BO.OpenCallInList
            {
                Id = c.Id,
                Address = c.Address,
                CallVolunteerDistance = CallManager.GetAerialDistance(Call_dal.Volunteer.Read(volunteerId).Address,c.Address)
            });

            if (sortBy != null)
            {
                openCalls = SortByField(sortBy, openCalls);
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
                TreatmentEndTime = ClockManager.Now
            }
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

            assignment.Status = DO.AssignmentStatus.COMPLETED;
            assignment.TreatmentEndTime = ClockManager.Now;

            Call_dal.Assignment.Update(assignment);
        }
        public BO.Call? Read(int id)
        {
            try
            {
                var doCall = Call_dal.Call.Read(id)
                    ?? throw new BO.NotFoundException("הקריאה לא נמצאה.");

                var assignment = Call_dal.Assignment.ReadAll()
                    .FirstOrDefault(a => a.VolunteerId == id && a.TreatmentEndTime == null);

                BO.CallInProgress? callInProgress = null;

                if (assignment != null)
                {
                    var call = Call_dal.Call.Read(assignment.CallId);
                    callInProgress = new BO.CallInProgress
                    {
                        CallId = call.Id,
                        Address = call.Address,
                        Description = call.Description
                    };
                }

                return new BO.Call
                (
                    //Id = do.Id,
                    //Name = doVolunteer.Name,
                    //Email = doVolunteer.Email,
                    //PhoneNumber = doVolunteer.Phone,
                    //CallInProgress = callInProgress
                    id = doCall.id,
                    TypeOfCall = doCall.typeOfCall,
                    Description = doCall.description,
                    Address = doCall.address,
                    Latitude = doCall.latitude,
                    Longitude = doCall.longitude,
                    OpeningTime = doCall.openingTime,
                    MaxClosingTime = doCall.maxClosingTime,
                    Status = doCall.status
                    //AssignedVolunteers = new List<BO.CallAssignInList>();
                );
            }
            catch (DO.DataAccessException ex)
            {
                throw new BO.DataAccessException("שגיאה בגישה לנתוני קריאות.", ex);
            }
        }

        public void Update(BO.Call boCall)
        {
            ValidateCall(boCall);

            var doCall = Call_dal.Call.Read(boCall.Id)
                ?? throw new BO.BlDoesNotExistException("קריאה לא נמצאה במערכת.");

            doCall.Description = boCall.Description;
            doCall.Status = (DO.CallStatus)boCall.Status;
            doCall.Address = boCall.Address;
            doCall.Latitude = boCall.Latitude;
            doCall.Longitude = boCall.Longitude;
            doCall.OpeningTime = boCall.OpeningTime;
            doCall.MaxClosingTime = boCall.MaxClosingTime;

            Call_dal.Call.Update(doCall);
        }

        // Helper methods for validation, filtering, sorting, and distance calculation...
    }
}
