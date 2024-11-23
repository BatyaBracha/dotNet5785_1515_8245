

namespace DO;

public record Assignment
(
    int Id,
    int CallId,
    int VolunteerId,
    DateTime TreatmentStartTime,
    DateTime? TreatmentEndTime,
    TypeOfTreatmentEnding? TypeOfTreatmentEnding
 )
{
    /// <summary>
    /// Default constructor with default property values
    /// </summary>
    public Assignment() : this(0, 0, 0, DateTime.MinValue, DateTime.MaxValue, default(TypeOfTreatmentEnding)){ }
}
