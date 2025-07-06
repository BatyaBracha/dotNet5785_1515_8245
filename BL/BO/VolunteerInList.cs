

namespace BO;

/// <summary>
/// Represents a volunteer in a summarized list context.
/// </summary>
public class VolunteerInList
{
    /// <summary>
/// The unique identifier for the volunteer.
/// </summary>
public int Id { get; set; }
    /// <summary>
/// The name of the volunteer.
/// </summary>
public string Name { get; set; }
    /// <summary>
/// Indicates whether the volunteer is active.
/// </summary>
public bool Active { get; set; }
    /// <summary>
/// The number of calls successfully handled by the volunteer.
/// </summary>
public int CallsDone { get; set; }
    /// <summary>
/// The number of calls canceled by the volunteer.
/// </summary>
public int CallsCanceled { get; set; }
    /// <summary>
/// The number of calls that became out of date for the volunteer.
/// </summary>
public int CallsOutOfDate { get; set; }
    /// <summary>
/// The ID of the call currently assigned to the volunteer, if any.
/// </summary>
public int? CallId { get; set; }
    /// <summary>
/// The type of call currently assigned to the volunteer.
/// </summary>
public TypeOfCall TypeOfCall { get; set; }
    /// <summary>
/// Returns a string representation of the volunteer in the list.
/// </summary>
public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, " +
               $"Active: {Active}, " +
               $"Calls Handeled: {CallsDone}, " +
               $"Calls Canceled: {CallsCanceled}, CallsOutOfDate: {CallsOutOfDate}, CallId: {CallId}, " +
               $"TypeOfCall: {TypeOfCall}";
    }

}
