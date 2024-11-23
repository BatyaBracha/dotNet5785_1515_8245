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
public call() : this(null) { }
public call() : this(0, default(AssignmentType), 0, DateTime.MinValue, null, null)
{

}

