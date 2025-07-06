namespace BO;
/// <summary>
/// Represents a call entity in the system.
/// </summary>
public class Call
{
    /// <summary>
/// Unique identifier for the call.
/// </summary>
public int Id { get; set; }
    /// <summary>
/// The type of the call.
/// </summary>
public TypeOfCall TypeOfCall { get; set; }
    /// <summary>
/// Description of the call.
/// </summary>
public string? Description { get; set; }
    /// <summary>
/// Address where the call takes place.
/// </summary>
public string? Address { get; set; }
    /// <summary>
/// Latitude of the call location.
/// </summary>
public double? Latitude { get; set; }
    /// <summary>
/// Longitude of the call location.
/// </summary>
public double? Longitude { get; set; }
    /// <summary>
/// The time when the call was opened.
/// </summary>
public DateTime OpeningTime { get; init; }
    /// <summary>
/// The latest time by which the call should be closed.
/// </summary>
public DateTime? MaxClosingTime { get; set; }
    /// <summary>
/// Current status of the call.
/// </summary>
public CallStatus Status { get; set; }
    /// <summary>
/// List of volunteers assigned to the call.
/// </summary>
public List<BO.CallAssignInList>? AssignedVolunteers { get; set; }
    /// <summary>
/// Initializes a new instance of the <see cref="Call"/> class.
/// </summary>
/// <param name="id">The unique identifier for the call.</param>
/// <param name="typeOfCall">The type of the call.</param>
/// <param name="description">The description of the call.</param>
/// <param name="address">The address where the call takes place.</param>
/// <param name="latitude">The latitude of the call location.</param>
/// <param name="longitude">The longitude of the call location.</param>
/// <param name="openingTime">The time when the call was opened.</param>
/// <param name="maxClosingTime">The latest time by which the call should be closed.</param>
/// <param name="status">The current status of the call.</param>
/// <param name="assignedVolunteers">List of volunteers assigned to the call.</param>
public Call(int id, TypeOfCall typeOfCall, string? description, string? address, double? latitude, double? longitude, DateTime openingTime, DateTime? maxClosingTime, CallStatus status, List<BO.CallAssignInList>? assignedVolunteers)
    {
        Id = id;
        TypeOfCall = typeOfCall;
        Description = description;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
        OpeningTime = openingTime;
        MaxClosingTime = maxClosingTime;
        Status = status;
        AssignedVolunteers = assignedVolunteers;
    }
    /// <summary>
/// Returns a string representation of the call.
/// </summary>
public override string ToString()
    {
        string assignedVolunteersNames = AssignedVolunteers != null && AssignedVolunteers.Any()
            ? string.Join(", ", AssignedVolunteers.Select(v => v.Name))
            : "None";

        return $"Id: {Id}, TypeOfCall: {TypeOfCall}, Description: {Description}, " +
               $"Address: {Address}, Latitude: {Latitude}, Longitude: {Longitude}, " +
               $"OpeningTime: {OpeningTime}, MaxClosingTime: {MaxClosingTime}, " +
               $"Status: {Status}, AssignedVolunteers: [{assignedVolunteersNames}]";
    }

}
