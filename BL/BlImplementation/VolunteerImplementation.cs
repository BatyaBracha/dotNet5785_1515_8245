

using BlApi;
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
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void MatchVolunteerToCall(int volunteerId, int callId)
    {
        throw new NotImplementedException();
    }

    public BO.Volunteer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.VolunteerInList> ReadAll(BO.Active? sort = null, BO.VolunteerFields? filter = null, object? value = null)
    {
        throw new NotImplementedException();
    }

    public void UnMatchVolunteerToCall(int volunteerId, int callId)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Volunteer boVolunteer)
    {
        throw new NotImplementedException();
    }
}
