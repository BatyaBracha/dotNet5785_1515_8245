

namespace DO;
/// <summary>
/// Assignment Entity
/// </summary>
/// <param name="Id">unique ID , indicates the allocation entity</param>
/// <param name="CallId">Represents a number that identifies the call that the volunteer chose to handle</param>
/// <param name="VolunteerId">represents the ID of the volunteer who chose to take care of the reading</param>
/// <param name="EntryTimeOfTreatment">Time (date and time) when the current call was processed. The time when for the first time the current volunteer chose to take care of her.</param>
/// <param name="EndingTimeOfTreatment"Time (date and time) when the current volunteer finished handling the current call</param>
/// <param name="endingTimeType">The manner in which the treatment of the current reading was completed by the current volunteer</param>

public record Assignment
(
    /// <summary>
/// Unique ID, indicates the allocation entity.
/// </summary>
int Id,
    /// <summary>
/// Represents a number that identifies the call that the volunteer chose to handle.
/// </summary>
int CallId,
    /// <summary>
/// Represents the ID of the volunteer who chose to take care of the reading.
/// </summary>
int VolunteerId,
    /// <summary>
/// Time (date and time) when the current call was processed. The time when for the first time the current volunteer chose to take care of her.
/// </summary>
DateTime TreatmentStartTime,
    /// <summary>
/// Time (date and time) when the current volunteer finished handling the current call.
/// </summary>
DateTime? TreatmentEndTime,
    /// <summary>
/// The manner in which the treatment of the current reading was completed by the current volunteer.
/// </summary>
TypeOfTreatmentEnding? TypeOfTreatmentEnding,
    /// <summary>
/// The current status of the assignment.
/// </summary>
AssignmentStatus AssignmentStatus
 )
{
    /// <summary>
    /// Default constructor with default property values
    /// </summary>
    public Assignment() : this(0, 0, 0, DateTime.MinValue, DateTime.MaxValue, default(TypeOfTreatmentEnding),AssignmentStatus.OPEN){ }
}
