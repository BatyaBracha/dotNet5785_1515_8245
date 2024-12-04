
namespace Dal;
using DalApi;
sealed public class DalList : IDal
{
    public IVolunteer Volunteer { get; } = new StudentImplementation();
    public ICall call { get; } = new StudentImplementation();
    public IAssignment Assignment { get; } = new StudentImplementation();
    public IConfig Config { get; } = new StudentImplementation();
    public void ResetDB()
    {
        Volunteer.DeleteAll();
        Call.DeleteAll();
        Assignment.DeleteAll();
        Config.Reset();
    }
}

