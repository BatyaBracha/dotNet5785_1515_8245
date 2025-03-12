

namespace BO;

public class Volunteer
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? CurrentAddress { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public Role Role { get; set; }
    public bool Active { get; set; }
    public double? MaxDistance { get; set; }
    public TypeOfDistance TypeOfDistance { get; set; }
    public int CallsDone { get; set; }
    public int CallsDeleted { get; set; }
    public int CallsChosenOutOfdate { get; set; }
    public BO.CallInProgress? CallInProgress { get; set; }
    public Volunteer(int id, string name, string phoneNumber, string email, string password, string? currentAddress, double? latitude, double? longitude, Role role, bool active, double? maxDistance, TypeOfDistance typeOfDistance, int callsDone, int callsDeleted, int callsChosenOutOfdate, CallInProgress? callInProgress)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        CurrentAddress = currentAddress;
        Latitude = latitude;
        Longitude = longitude;
        Role = role;
        Active = active;
        MaxDistance = maxDistance;
        TypeOfDistance = typeOfDistance;
        CallsDone = callsDone;
        CallsDeleted = callsDeleted;
        CallsChosenOutOfdate = callsChosenOutOfdate;
        CallInProgress = callInProgress;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, PhoneNumber: {PhoneNumber}, Email: {Email}, " +
               $"CurrentAddress: {CurrentAddress}, Latitude: {Latitude}, Longitude: {Longitude}, " +
               $"Role: {Role}, Active: {Active}, MaxDistance: {MaxDistance}, " +
               $"TypeOfDistance: {TypeOfDistance}, CallsDone: {CallsDone}, " +
               $"CallsDeleted: {CallsDeleted}, CallsChosenOutOfdate: {CallsChosenOutOfdate}, " +
               $"CallInProgress: {CallInProgress}";
    }
}

