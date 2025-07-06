using DalApi;
using System.Diagnostics;
namespace Dal;

/// <summary>
/// Implements the main DAL XML class for accessing data entities.
/// </summary>
sealed internal class DalXml : IDal
{
    /// <summary>
    /// Gets the singleton instance of the DalXml class.
    /// </summary>
        /// <summary>
    /// Gets the singleton instance of the DalXml class.
    /// </summary>
    public static IDal Instance { get; } = new DalXml();

    /// <summary>
    /// Initializes a new instance of the DalXml class.
    /// </summary>
        /// <summary>
    /// Initializes a new instance of the DalXml class.
    /// </summary>
    private DalXml() { }

    /// <summary>
    /// Gets the volunteer data access object.
    /// </summary>
        /// <summary>
    /// Gets the volunteer data access object.
    /// </summary>
    public IVolunteer Volunteer { get; } = new VolunteerImplementation();

    /// <summary>
    /// Gets the call data access object.
    /// </summary>
        /// <summary>
    /// Gets the call data access object.
    /// </summary>
    public ICall Call { get; } = new CallImplementation();

    /// <summary>
    /// Gets the assignment data access object.
    /// </summary>
        /// <summary>
    /// Gets the assignment data access object.
    /// </summary>
    public IAssignment Assignment { get; } = new AssignmentImplementation();

    /// <summary>
    /// Gets the configuration data access object.
    /// </summary>
        /// <summary>
    /// Gets the configuration data access object.
    /// </summary>
    public IConfig Config { get; } = new ConfigImplementation();

        /// <summary>
    /// Resets the database by deleting all volunteers, calls, assignments, and resetting configuration.
    /// </summary>
    public void ResetDB()
    {
        Volunteer.DeleteAll();
        Call.DeleteAll();
        Assignment.DeleteAll();
        Config.Reset();
    }

}
