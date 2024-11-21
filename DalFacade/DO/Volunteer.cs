//Module Volunteer.cs
using System.Xml;

namespace DO;
/// <summary>
///  Volunteer Entity represents a volunteer with all its props
/// </summary>
/// <param name = "Id" > Personal unique ID of the volunteer(as in national id card)</param>
/// <param name="Name">Private Name of the volunteer</param>
/// <param name="RegistrationDate">Registration date of the volunteer into the graduation program</param>
/// <param name="Alias">volunteer’s alias name (default empty)</param>
/// <param name="IsActive">whether the volunteer is active in volunteering (default true)</param>
/// <param name="BirthDate">volunteer’s birthday (default empty)</param>
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
    double MaxDistance,
    TypeOfDistance TypeOfDistance
    );


