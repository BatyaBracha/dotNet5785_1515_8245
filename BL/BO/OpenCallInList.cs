

using System.Xml.Linq;

namespace BO;

/// <summary>
/// Represents an open call in a list context.
/// </summary>
public class OpenCallInList
{
    /// <summary>
/// The unique identifier for the open call.
/// </summary>
public int Id { get; set; }
    /// <summary>
/// The type of the open call.
/// </summary>
public TypeOfCall TypeOfCall { get; set; }
    /// <summary>
/// The description of the open call.
/// </summary>
public string? Description { get; set; }
    /// <summary>
/// The address where the call takes place.
/// </summary>
public string Address { get; set; }
    /// <summary>
/// The time when the call was opened.
/// </summary>
public DateTime OpeningTime { get; init; }
    /// <summary>
/// The latest time by which the call should be closed.
/// </summary>
public DateTime? MaxCloseingTime { get; set; }
    /// <summary>
/// The distance between the call and the volunteer.
/// </summary>
public double CallVolunteerDistance { get;set; }
    /// <summary>
/// Returns a string representation of the open call in the list.
/// </summary>
public override string ToString()
    {
        return $"Id: {Id}, TypeOfCall: {TypeOfCall}, Description: {Description}, " +
               $"Address: {Address}, OpeningTime: {OpeningTime}, " +
               $"MaxCloseingTime: {MaxCloseingTime}, CallVolunteerDistance: {CallVolunteerDistance}";
    }

}
