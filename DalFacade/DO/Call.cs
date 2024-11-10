namespace DO;

public record Call
(

    int Id,
    Enum TypeOfCall,
    string? Description,
    string Address,
    double latitude,
    double longitude,
    DateTime OpeningTime,
    DateTime? MaxClosingTime
);
