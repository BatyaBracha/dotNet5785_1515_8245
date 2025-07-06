

namespace DalApi;

/// <summary>
/// Defines the main DAL interface for accessing data entities.
/// </summary>
public interface IDal
{
    IVolunteer Volunteer { get; }
    ICall Call { get; }
    IAssignment Assignment { get; }
    IConfig Config { get; }
    void ResetDB();
}
