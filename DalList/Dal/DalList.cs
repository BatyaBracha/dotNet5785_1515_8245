namespace Dal;

using DalApi;

/// <summary>
/// Implements the main data access layer for DalList.
/// </summary>
public class DalList : IDal
{
    /// <summary>
    /// Gets the singleton instance of the DalList class.
    /// </summary>
    public static IDal Instance { get; } = new DalList();

    /// <summary>
    /// Initializes a new instance of the DalList class.
    /// </summary>
    private DalList() { }
    /// <summary>
    /// Gets the volunteer data entity.
    /// </summary>
    public IVolunteer Volunteer { get; } = new VolunteerImplementation();
    /// <summary>
    /// Gets the call data entity.
    /// </summary>
    public ICall Call { get; } = new CallImplementation();
    /// <summary>
    /// Gets the assignment data entity.
    /// </summary>
    public IAssignment Assignment { get; } = new AssignmentImplementation();
    /// <summary>
    /// Gets the configuration data entity.
    /// </summary>
    public IConfig Config { get; } = new ConfigImplementation();
    public void ResetDB()
    {
        Volunteer.DeleteAll();
        Call.DeleteAll();
        Assignment.DeleteAll();
        Config.Reset();
    }
}

