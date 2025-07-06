

using System.Xml.Linq;

namespace BO;

/// <summary>
/// Represents a closed call in a list context.
/// </summary>
public class ClosedCallInList
{
    /// <summary>
/// The unique identifier for the closed call.
/// </summary>
public int Id { get; set; }
    /// <summary>
/// The type of the closed call.
/// </summary>
public TypeOfCall TypeOfCall { get; set; }
    /// <summary>
/// The address where the call took place.
/// </summary>
public string Address { get; set; }
    /// <summary>
/// The time when the call was opened.
/// </summary>
public DateTime OpeningTime { get; init; }
    /// <summary>
/// The time when the volunteer entered treatment for the call.
/// </summary>
public DateTime EntryTimeForTreatment { get; set; }
    /// <summary>
/// The maximum allowed closing time for the call.
/// </summary>
public DateTime MaxCloseingTime { get; set; }
    /// <summary>
/// The actual time when the treatment ended, if applicable.
/// </summary>
public DateTime? ActualTreatmentEndTime { get; set; }
    /// <summary>
/// The manner in which the treatment ended for the call.
/// </summary>
public TypeOfTreatmentEnding? TypeOfTreatmentEnding { get; set; }
    /// <summary>
/// Returns a string representation of the closed call in the list.
/// </summary>
public override string ToString()
    {
        return $"Id: {Id}, TypeOfCall: {TypeOfCall}, Address: {Address}, OpeningTime: {OpeningTime}, " +
               $"EntryTimeForTreatment: {EntryTimeForTreatment}, MaxCloseingTime: {MaxCloseingTime}, ActualTreatmentEndTime: {ActualTreatmentEndTime}, " +
               $"TypeOfTreatmentEnding: {TypeOfTreatmentEnding} ";
    }
}
