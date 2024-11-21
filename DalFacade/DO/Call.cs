namespace DO;

public record Call
(

    int Id,
    TypeOfCall TypeOfCall,
    string? Description,
    string Address,
    double latitude,
    double longitude,
    DateTime OpeningTime,
    DateTime? MaxClosingTime
);
