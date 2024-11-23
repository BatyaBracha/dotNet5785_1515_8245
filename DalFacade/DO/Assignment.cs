

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
    /// Default constructor for stage 3
    /// </summary>
    public Assignment() : this(null) { }
    public Assignment() : this(0, default(AssignmentType), 0, DateTime.MinValue, null, null)
    {
        
    }
}

