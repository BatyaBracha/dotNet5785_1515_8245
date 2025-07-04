

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
    public override string ToString()
    {
        return $"CallId: {CallId}, Type Of Call: {TypeOfCall}, \n" +
               $"Description: {Description}, Address: {Address}, \n" +
               $"Opening Time: {TimeOfOpening},\n Max Finish Time: {MaxFinishTime}, \n" +
               $"Time Of Entry To Treatment: {TimeOfEntryToTreatment}, \n" +
               $"Call Volunteer Distance: {CallVolunteerDistance}, \nStatus: {Status}";
    }
}
