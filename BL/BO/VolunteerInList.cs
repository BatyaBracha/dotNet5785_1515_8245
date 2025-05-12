

namespace BO;

public class VolunteerInList
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    public int CallsDone { get; set; }
    public int CallsCanceled { get; set; }
    public int CallsOutOfDate { get; set; }
    public int? CallId { get; set; }
    public TypeOfCall TypeOfCall { get; set; }
    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, " +
               $"Active: {Active}, " +
               $"Calls Handeled: {CallsDone}, " +
               $"Calls Canceled: {CallsCanceled}, CallsOutOfDate: {CallsOutOfDate}, CallId: {CallId}, " +
               $"TypeOfCall: {TypeOfCall}";
    }

}
