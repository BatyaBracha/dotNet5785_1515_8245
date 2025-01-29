namespace BO;
public class Call
{
    public int Id {  get; set; }
    public TypeOfCall TypeOfCall { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public double? Latitude {  get; set; }
    public double? Longitude { get; set; }
    public DateTime OpeningTime { get; init; }
    public DateTime? MaxClosingTime { get; set; }
    public CallStatus Status { get; set; }
    public List<BO.CallAssignInList>? AssignedVolunteers { get; set; }
}
