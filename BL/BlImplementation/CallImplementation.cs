

using BlApi;
namespace BlImplementation;

internal class CallImplementation : ICall
{
    private readonly DalApi.IDal Call_dal = DalApi.Factory.Get;
    public void ChoosingACallForTreatment(int volunteerId, int AssignmentId)
    {
        throw new NotImplementedException();
    }

    public void Create(BO.Call boCall)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Call GetCallDetails(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<int> GetCallsCount()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.CallInList> GetCallsList(Enum? filterBy, object? filter, Enum? sortBy)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.ClosedCallInList> GetClosedCallsHandledByTheVolunteer(int volunteerId, Enum? sortBy)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.OpenCallInList> GetOpenCallsCanBeSelectedByAVolunteer(int volunteerId, Enum? filterBy, Enum? sortBy)
    {
        throw new NotImplementedException();
    }

    public void TreatmentCancellationUpdate(int volunteerId, int AssignmentId)
    {
        throw new NotImplementedException();
    }

    public void TreatmentCompletionUpdate(int volunteerId, int AssignmentId)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Call boCall)
    {
        throw new NotImplementedException();
    }
}
