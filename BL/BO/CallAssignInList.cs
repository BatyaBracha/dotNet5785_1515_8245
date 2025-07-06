namespace BO;

/// <summary>
/// Represents an assignment of a volunteer to a call in a list context.
/// </summary>
public class CallAssignInList
{
    /// <summary>
/// The ID of the assigned volunteer.
/// </summary>
public int? VolunteerId {  get; set; }
    /// <summary>
/// The name of the assigned volunteer.
/// </summary>
public string? Name {  get; set; }
    /// <summary>
/// The time when the assignment started.
/// </summary>
public DateTime TimeOfStarting { get; init; }
    /// <summary>
/// The time when the assignment ended, if applicable.
/// </summary>
public DateTime? TimeOfEnding { get; set; }
    /// <summary>
/// The manner in which the treatment ended for this assignment.
/// </summary>
public TypeOfTreatmentEnding? TypeOfTreatmentEnding {  get; set; }
    /// <summary>
/// Returns a string representation of the assignment in the list.
/// </summary>
public override string ToString()
    {
        return $"VolunteerId: {VolunteerId}, Name: {Name}, " +
               $"TimeOfStarting: {TimeOfStarting}, TimeOfEnding: {TimeOfEnding}, " +
               $"TypeOfTreatmentEnding: {TypeOfTreatmentEnding}";
    }
}
