

namespace BO;

public class VolunteerInList
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    public int CallsHandeled { get; set; }
    public int CallsCanceled { get; set; }
    public int CallsOutOfDate { get; set; }
    public int? CallId { get; set; }
    public TypeOfCall TypeOfCall { get; set; }
}
