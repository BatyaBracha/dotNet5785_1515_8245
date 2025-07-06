

namespace BO;

/// <summary>
/// Represents a volunteer in the system.
/// </summary>
/// <summary>
/// Represents a volunteer entity with all relevant properties.
public class Volunteer
{
    /// <summary>
    /// Gets or sets the unique ID of the volunteer.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the volunteer.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the volunteer.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the email address of the volunteer.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the volunteer.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the current address of the volunteer.
    /// </summary>
    public string? CurrentAddress { get; set; }

    /// <summary>
    /// Gets or sets the latitude of the volunteer's current address.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude of the volunteer's current address.
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    /// Gets or sets the role of the volunteer in the system.
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the volunteer is active.
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// Gets or sets the maximum distance the volunteer is willing to travel.
    /// </summary>
    public double? MaxDistance { get; set; }

/// </summary>
//public double? MaxDistance { get; set; }
    /// <summary>
/// The type of distance calculation for the volunteer.
/// </summary>
        /// <summary>
        /// Gets or sets the type of distance for the volunteer.
        /// </summary>
        public TypeOfDistance TypeOfDistance { get; set; }
    /// <summary>
/// The number of calls successfully handled by the volunteer.
/// </summary>
public int CallsDone { get; set; }
    /// <summary>
/// The number of calls canceled by the volunteer.
/// </summary>
public int CallsCanceled { get; set; }
    /// <summary>
/// The number of calls that became out of date for the volunteer.
/// </summary>
public int CallsOutOfdate { get; set; }
    /// <summary>
/// The call currently in progress for the volunteer, if any.
/// </summary>
public BO.CallInProgress? CallInProgress { get; set; }
    /// <summary>
/// Initializes a new instance of the <see cref="Volunteer"/> class.
/// </summary>
/// <param name="id">The unique identifier for the volunteer.</param>
/// <param name="name">The name of the volunteer.</param>
/// <param name="phoneNumber">The phone number of the volunteer.</param>
/// <param name="email">The email address of the volunteer.</param>
/// <param name="password">The password of the volunteer.</param>
/// <param name="currentAddress">The current address of the volunteer.</param>
/// <param name="latitude">The latitude of the volunteer's current address.</param>
/// <param name="longitude">The longitude of the volunteer's current address.</param>
/// <param name="role">The role of the volunteer in the system.</param>
/// <param name="active">Indicates whether the volunteer is active.</param>
/// <param name="maxDistance">The maximum distance the volunteer is willing to travel.</param>
/// <param name="typeOfDistance">The type of distance calculation for the volunteer.</param>
/// <param name="callsDone">The number of calls successfully handled by the volunteer.</param>
/// <param name="callsDeleted">The number of calls canceled by the volunteer.</param>
/// <param name="callsChosenOutOfdate">The number of calls that became out of date for the volunteer.</param>
/// <param name="callInProgress">The call currently in progress for the volunteer, if any.</param>
public Volunteer(int id, string name, string phoneNumber, string email, string password, string? currentAddress, double? latitude, double? longitude, Role role, bool active, double? maxDistance, TypeOfDistance typeOfDistance, int callsDone, int callsDeleted, int callsChosenOutOfdate, CallInProgress? callInProgress)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        CurrentAddress = currentAddress;
        Latitude = latitude;
        Longitude = longitude;
        Role = role;
        Active = active;
        MaxDistance = maxDistance;
        TypeOfDistance = typeOfDistance;
        CallsDone = callsDone;
        CallsCanceled = callsDeleted;
        CallsOutOfdate = callsChosenOutOfdate;
        CallInProgress = callInProgress;
    }

    /// <summary>
/// Returns a string representation of the volunteer.
/// </summary>
public override string ToString()
    {
        return $"Id: {Id},\n" +
               $"Name: {Name},\n" +
               $"Phone Number: {PhoneNumber},\n" +
               $"Email: {Email},\n" +
               $"Current Address: {CurrentAddress},\n" +
               $"Latitude: {Latitude},\n" +
               $"Longitude: {Longitude},\n" +
               $"Role: {Role},\n" +
               $"Active: {Active},\n" +
               $"Max Distance: {MaxDistance},\n" +
               $"Type of Distance: {TypeOfDistance},\n" +
               $"Calls Done: {CallsDone},\n" +
               $"Calls Canceled: {CallsCanceled},\n" +
               $"Calls Out of Date: {CallsOutOfdate},\n" +
               $"Call In Progress: {CallInProgress}";
    }
}

