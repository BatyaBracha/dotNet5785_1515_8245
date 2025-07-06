namespace BO;

/// <summary>
/// Represents an assignment of a volunteer to a call in a list context.
/// </summary>
/// <summary>
/// Represents a call assignment entry in a list.
/// </summary>
public class CallAssignInList
{
    /// <summary>
    /// Gets or sets the ID of the assigned volunteer.
    /// </summary>
    public int? VolunteerId {  get; set; }

    /// <summary>
    /// Gets or sets the ID of the call.
    /// </summary>
    public int CallId { get; set; }

    /// <summary>
    /// Gets or sets the name of the assigned volunteer.
    /// </summary>
    public string? Name {  get; set; }

    /// <summary>
    /// Gets or sets the time when the assignment started.
    /// </summary>
    public DateTime TimeOfStarting { get; init; }

    /// <summary>
    /// Gets or sets the time of assignment.
    /// </summary>
    public DateTime AssignmentTime { get; set; }

    /// <summary>
    /// Gets or sets the time when the assignment ended, if applicable.
    /// </summary>
    public DateTime? TimeOfEnding { get; set; }

    /// <summary>
    /// Gets or sets the manner in which the treatment ended for this assignment.
    /// </summary>
    public TypeOfTreatmentEnding? TypeOfTreatmentEnding {  get; set; }

    /// <summary>
    /// Returns a string representation of the assignment in the list.
    /// </summary>
    /// <returns>A string representation of the assignment.</returns>
    //public override string ToString()
/// </summary>
public override string ToString()
    {
        return $"VolunteerId: {VolunteerId}, Name: {Name}, " +
               $"TimeOfStarting: {TimeOfStarting}, TimeOfEnding: {TimeOfEnding}, " +
               $"TypeOfTreatmentEnding: {TypeOfTreatmentEnding}";
    }
}
