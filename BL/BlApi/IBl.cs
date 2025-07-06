namespace BlApi;

/// <summary>
/// Interface for the main BL (Business Logic) operations.
/// </summary>
/// <summary>
/// Interface for the main business logic layer.
/// </summary>
public interface IBl 

{
    /// <summary>
/// Provides access to volunteer-related operations.
/// </summary>
IVolunteer Volunteer { get; }
    /// <summary>
/// Provides access to call-related operations.
/// </summary>
ICall Call { get; }
    /// <summary>
/// Provides access to administrative operations.
/// </summary>
IAdmin Admin { get; }

}
