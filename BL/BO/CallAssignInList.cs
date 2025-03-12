namespace BO;

public class CallAssignInList
{
    public int? VolunteerId {  get; set; }
    public string? Name {  get; set; }
    public DateTime TimeOfStarting { get; init; }
    public DateTime? TimeOfEnding { get; set; }
    public TypeOfTreatmentEnding? TypeOfTreatmentEnding {  get; set; }
    public override string ToString()
    {
        return $"VolunteerId: {VolunteerId}, Name: {Name}, " +
               $"TimeOfStarting: {TimeOfStarting}, TimeOfEnding: {TimeOfEnding}, " +
               $"TypeOfTreatmentEnding: {TypeOfTreatmentEnding}";
    }
}
