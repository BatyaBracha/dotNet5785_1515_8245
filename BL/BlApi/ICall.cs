
using static BlImplementation.CallImplementation;

namespace BlApi;

/// <summary>
/// Interface for call-related operations in the BL layer.
/// </summary>
/// <summary>
/// Interface for call-related operations in the business logic layer.
/// </summary>
public interface ICall : IObservable //stage 5 הרחבת ממשק

{
    IEnumerable<object> GetCallsCount();
    IEnumerable<BO.CallInList> ReadAll(Enum? filterBy, object? filter, Enum? sortBy);
    BO.Call GetCallDetails(int id);
    void Update(BO.Call boCall);
    void Delete(int id);
    void Create(BO.Call boCall);
    IEnumerable<BO.ClosedCallInList> GetClosedCallsHandledByTheVolunteer(int volunteerId,Enum? filterBy, object? filterValue, Enum? sortBy);
    IEnumerable<BO.OpenCallInList> GetOpenCallsCanBeSelectedByAVolunteer(int volunteerId, Enum? filterBy, object? filterValue, Enum? sortBy);
    void TreatmentCompletionUpdate(int volunteerId, int AssignmentId);
    void TreatmentCancellationUpdate(int volunteerId, int AssignmentId);
    void ChoosingACallForTreatment(int volunteerId, int CallId);
    public class CallStatusCount;

}
