

namespace BO;

/// <summary>
/// Represents a call in a summarized list context.
/// </summary>
public class CallInList
{
    /// <summary>
/// The unique identifier for the call in the list.
/// </summary>
public int? Id {  get; set; }
    /// <summary>
/// The ID of the call.
/// </summary>
public int CallId { get; set; }
    /// <summary>
/// The type of the call.
/// </summary>
public TypeOfCall TypeOfCall { get; set; }
    /// <summary>
/// The time when the call was opened.
/// </summary>
public DateTime OpeningTime { get; init; }
    /// <summary>
/// The time left until the call should be closed, if applicable.
/// </summary>
public TimeSpan? TimeLeft { get; set; }
    /// <summary>
/// The name of the last volunteer who handled the call.
/// </summary>
public string? LastVolunteerName { get; set; }
    /// <summary>
/// The duration of the treatment for the call, if applicable.
/// </summary>
public TimeSpan? TreatmentDuration { get; set; }
    /// <summary>
/// The current status of the call.
/// </summary>
public CallStatus Status { get; set; }
    /// <summary>
/// The total number of assignments for the call.
/// </summary>
public int AssignmentsSum {  get; set; }
    /// <summary>
/// Returns a string representation of the call in the list.
/// </summary>
public override string ToString()
    {
        return $"CallId: {CallId}, TypeOfCall: {TypeOfCall}, " +
               $"OpeningTime: {OpeningTime}, TimeLeft: {TimeLeft}, " +
               $"LastVolunteerName: {LastVolunteerName}, TreatmentDuration: {TreatmentDuration}, " +
               $"Status: {Status}, AssignmentsSum: {AssignmentsSum}";
    }
}
