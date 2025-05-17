

namespace BO;

public class CallInList
{
    public int? Id {  get; set; }
    public int CallId { get; set; }
    public TypeOfCall TypeOfCall { get; set; }
    public DateTime OpeningTime { get; init; }
    public TimeSpan? TimeLeft { get; set; }
    public string? LastVolunteerName { get; set; }
    public TimeSpan? TreatmentDuration { get; set; }
    public CallStatus Status { get; set; }
    public int AssignmentsSum {  get; set; }
    public override string ToString()
    {
        return $"CallId: {CallId}, TypeOfCall: {TypeOfCall}, " +
               $"OpeningTime: {OpeningTime}, TimeLeft: {TimeLeft}, " +
               $"LastVolunteerName: {LastVolunteerName}, TreatmentDuration: {TreatmentDuration}, " +
               $"Status: {Status}, AssignmentsSum: {AssignmentsSum}";
    }
}
