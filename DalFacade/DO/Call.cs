using Microsoft.VisualBasic;

namespace DO;

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
    DateTime? MaxClosingTime = null
)
{
    /// <summary>
    /// Default constructor with default property values
    /// </summary>
    public Call() : this(0, default(TypeOfCall), "noDescription", "noAddress", 0, 0,null, DateTime.MinValue){}
}