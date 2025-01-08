﻿

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
}
