

namespace BO;

/// <summary>
/// Represents a call that is currently in progress.
/// </summary>
public class CallInProgress
{
    /// <summary>
/// The unique identifier for the call in progress.
/// </summary>
public int Id {  get; set; }
    /// <summary>
/// The ID of the call.
/// </summary>
public int CallId { get; init; }
    /// <summary>
/// The type of the call.
/// </summary>
public TypeOfCall TypeOfCall { get; set; }
    /// <summary>
/// The description of the call.
/// </summary>
public string? Description { get; set; }
    /// <summary>
/// The address of the call.
/// </summary>
public string Address { get; set; }
    /// <summary>
/// The time when the call was opened.
/// </summary>
public DateTime TimeOfOpening { get; init; }
    /// <summary>
/// The maximum allowed finish time for the call.
/// </summary>
public DateTime? MaxFinishTime { get; set; }
    /// <summary>
/// The time when the volunteer entered treatment for the call.
/// </summary>
public DateTime TimeOfEntryToTreatment { get; set; }
    /// <summary>
/// The distance between the call and the volunteer.
/// </summary>
public double CallVolunteerDistance { get; set; }
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
