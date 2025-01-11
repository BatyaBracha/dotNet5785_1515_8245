

namespace BO;

public class ClosedCallInList
{
    public int Id { get; set; }
    public TypeOfCall TypeOfCall { get; set; }
    public string Address { get; set; }
    public DateTime OpeningTime { get; init; }
    public DateTime EntryTimeForTreatment { get; set; }
    public DateTime MaxCloseingTime { get; set; }
    public DateTime? ActualTreatmentEndTime { get; set; }
    public TypeOfTreatmentEnding? TypeOfTreatmentEnding { get; set; }

}
