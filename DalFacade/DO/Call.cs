
namespace DO;
/// <summary>
/// Call Entity
/// </summary>
/// <param name="Id">unique ID , indicates the allocation entity</param>
/// <param name="CallType">The type of calling</param>
/// <param name="VerbalDescription">Description of the calling. Additional details about the calling</param>
/// <param name="FullAddressCall">Full address.</param>
/// <param name="Latitude">Indicate how far a point on the earth is north or south of the equator </param>
/// <param name="Longitude">Indicate how far a point on the earth is east or west of the equator </param>
/// <param name="OpeningTime">Time (date and hour) when the reading was opened by the administrator</param>
/// <param name="MaxTimeFinishCalling">"Time (date and hour) by which the reading must be closed </param>

public record Call
(
    int Id,
    TypeOfCall TypeOfCall,
    string? Description,
    string Address,
    double? latitude,
    double? longitude,
    TimeSpan? riskRange,
    DateTime OpeningTime,
    CallStatus Status,
    DateTime? MaxClosingTime

)
{
    /// <summary>
    /// Default constructor with default property values
    /// </summary>
    public Call() : this(0, default(TypeOfCall), "noDescription", "noAddress", 0, 0,null, DateTime.MinValue,CallStatus.OPEN,null){}
}