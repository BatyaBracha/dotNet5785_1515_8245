

namespace BO;

public class CallInProgress
{
    public int Id {  get; set; }
    public int CallId { get; init; }
    public TypeOfCall TypeOfCall { get; set; }
    public string? Description { get; set; }

    public string Address { get; set; }
    public DateTime TimeOfOpening { get; init; }
    public DateTime? MaxFinishTime { get; set; }
    public DateTime TimeOfEntryToTreatment { get; set; }
    public double CallVolunteerDistance { get; set; }
    public Status Status { get; set; }
}
