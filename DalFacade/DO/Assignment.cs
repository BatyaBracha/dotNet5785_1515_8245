

namespace DO;

public record Assignment
(
    int Id,
    int CallId,
    int VolunteerId,
    DateTime TreatmentStartTime,
    DateTime? TreatmentEndTime,
    Enum? TypeOfTreatmentEnding
 );
