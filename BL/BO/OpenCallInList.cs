

using System.Xml.Linq;

namespace BO;

public class OpenCallInList
{
    public int Id { get; set; }
    public TypeOfCall TypeOfCall { get; set; }
    public string? Description { get; set; }
    public string Address { get; set; }
    public DateTime OpeningTime { get; init; }
    public DateTime? MaxCloseingTime { get; set; }
    public double CallVolunteerDistance { get; set; }
    public override string ToString()
    {
        return $"Id: {Id}, TypeOfCall: {TypeOfCall}, Description: {Description}, " +
               $"Address: {Address}, OpeningTime: {OpeningTime}, " +
               $"MaxCloseingTime: {MaxCloseingTime}, CallVolunteerDistance: {CallVolunteerDistance}";
    }

}