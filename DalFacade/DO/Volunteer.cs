//Module Volunteer.cs
using System.Xml;

namespace DO;
/// <summary>
/// Volunteer Entity
/// </summary>
/// <param name="Id">unique ID (created automatically - provide 0 as an argument)</param>
/// <param name="FullName">FullName of volunteer</param>
/// <param name="Phone">PhoneNumber of volunteer</param>
/// <param name="Email">Email of volunteer</param>
/// <param name="Password">Password of volunteer</param>
/// <param name="CurrentAddress">CurrentAddress of volunteer</param>
/// <param name="Position">Position of volunteer - can be a manager or a volunteer</param>
/// <param name="Latitude">Indicate how far a point on the earth is north or south of the equator </param>
/// <param name="Longitude">Indicate how far a point on the earth is east or west of the equator </param>
/// <param name="IsActive">Indicate if  the volunteer is active or not </param>
/// <param name="MaxDistance">The  MaxDistance  to receive a call</param>
/// <param name="DistanceType">DistanceType - type of Distance ,default - AIR </param>
///
public record Volunteer
   (
     int Id,
    string Name,
    string Phone,
    string Email,
    string? Password,
    string? Address,
    double? latitude,
    double? longitude,
    Role Role,
    bool Active,
    double? MaxDistance,
    TypeOfDistance TypeOfDistance
    )
{
    /// <summary>
    /// Default constructor with default property values
    /// </summary>
    public Volunteer() : this(0, "noName", "noPhone", "noEmail", null, null, null,null,default(Role),false,0,default(TypeOfDistance))
    {
        // Additional initialization if needed
    }
}


