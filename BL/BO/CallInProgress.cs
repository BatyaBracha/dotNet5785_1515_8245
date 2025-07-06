

namespace BO;

/// <summary>
/// Represents a call that is currently in progress.
/// </summary>
/// <summary>
/// Represents a call currently in progress.
/// </summary>
public class CallInProgress
{
    /// <summary>
    /// Gets or sets the unique identifier for the call in progress.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the call.
    /// </summary>
    public int CallId { get; init; }

    /// <summary>
    /// Gets or sets the type of the call.
    /// </summary>
    public TypeOfCall TypeOfCall { get; set; }

    /// <summary>
    /// Gets or sets the description of the call.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the address of the call.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the time when the call was opened.
    /// </summary>
    public DateTime TimeOfOpening { get; init; }

    /// <summary>
    /// Gets or sets the maximum allowed finish time for the call.
    /// </summary>
    public DateTime? MaxFinishTime { get; set; }

    /// <summary>
    /// Gets or sets the time when the volunteer entered treatment for the call.
    /// </summary>
    public DateTime TimeOfEntryToTreatment { get; set; }

    /// <summary>
    /// Gets or sets the distance between the call and the volunteer.
    /// </summary>
    public double CallVolunteerDistance { get; set; }

/// </summary>
//public double CallVolunteerDistance { get; set; }
    /// <summary>
/// The current status of the call in progress.
/// </summary>
public Status Status { get; set; }
    /// <summary>
/// Returns a string representation of the call in progress.
/// </summary>
public override string ToString()
    {
        return $"CallId: {CallId}, Type Of Call: {TypeOfCall}, \n" +
               $"Description: {Description}, Address: {Address}, \n" +
               $"Opening Time: {TimeOfOpening},\n Max Finish Time: {MaxFinishTime}, \n" +
               $"Time Of Entry To Treatment: {TimeOfEntryToTreatment}, \n" +
               $"Call Volunteer Distance: {CallVolunteerDistance}, \nStatus: {Status}";
    }
}
