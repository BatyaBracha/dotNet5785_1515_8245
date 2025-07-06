

namespace BO;

/// <summary>
/// Represents a call in a summarized list context.
/// </summary>
/// <summary>
/// Represents a call item in a list with summary information.
/// </summary>
public class CallInList
{
    /// <summary>
    /// Gets or sets the unique identifier for the call in the list.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the call.
    /// </summary>
    public int CallId { get; set; }

    /// <summary>
    /// Gets or sets the type of the call.
    /// </summary>
    public TypeOfCall TypeOfCall { get; set; }

    /// <summary>
    /// Gets or sets the time when the call was opened.
    /// </summary>
    public DateTime OpeningTime { get; init; }

    /// <summary>
    /// Gets or sets the time left until the call should be closed, if applicable.
    /// </summary>
    public TimeSpan? TimeLeft { get; set; }

    /// <summary>
    /// Gets or sets the name of the last volunteer who handled the call.
    /// </summary>
    public string? LastVolunteerName { get; set; }

    /// <summary>
    /// Gets or sets the duration of the treatment for the call, if applicable.
    /// </summary>
    public TimeSpan? TreatmentDuration { get; set; }

    /// <summary>
    /// Gets or sets the current status of the call.
    /// </summary>
    public CallStatus Status { get; set; }

/// </summary>
//public CallStatus Status { get; set; }
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
