
namespace BlImplementation;
using BlApi;

/// <summary>
/// Implements the main BL (Business Logic) operations.
/// </summary>
internal class Bl : IBl
{
    /// <summary>
    /// Gets the Admin implementation.
    /// </summary>
    /// <value>The Admin implementation.</value>
    public IAdmin Admin { get; } = new AdminImplementation();

    /// <summary>
    /// Gets the Call implementation.
    /// </summary>
    /// <value>The Call implementation.</value>
    public ICall Call { get; } = new CallImplementation();

    /// <summary>
    /// Gets the Volunteer implementation.
    /// </summary>
    /// <value>The Volunteer implementation.</value>
    public IVolunteer Volunteer { get; } = new VolunteerImplementation();
}
