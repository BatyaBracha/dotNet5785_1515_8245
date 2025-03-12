namespace BO;
public class Call
{
    public int Id { get; set; }
    public TypeOfCall TypeOfCall { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public DateTime OpeningTime { get; init; }
    public DateTime? MaxClosingTime { get; set; }
    public CallStatus Status { get; set; }
    public List<BO.CallAssignInList>? AssignedVolunteers { get; set; }
    public Call(int id, TypeOfCall typeOfCall, string? description, string? address, double? latitude, double? longitude, DateTime openingTime, DateTime? maxClosingTime, CallStatus status, List<BO.CallAssignInList>? AssignedVolunteers)
    {
        Id = id;
        TypeOfCall = typeOfCall;
        Description = description;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
        OpeningTime = openingTime;
        MaxClosingTime = maxClosingTime;
        Status = status;
        AssignedVolunteers = AssignedVolunteers;
    }
    public override string ToString()
    {
        return $"Id: {Id}, TypeOfCall: {TypeOfCall}, Description: {Description}, " +
               $"Address: {Address}, Latitude: {Latitude}, Longitude: {Longitude}, " +
               $"OpeningTime: {OpeningTime}, MaxClosingTime: {MaxClosingTime}, " +
               $"Status: {Status}, AssignedVolunteers: {AssignedVolunteers}";
    }

}
