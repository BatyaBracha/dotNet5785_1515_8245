
namespace BO;

[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message, string? propertyName) : base($"{message} (Property: {propertyName})") { }
}


[Serializable]

public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
}

[Serializable]

public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible(string? message) : base(message) { }
}

[Serializable]

public class NullException : Exception
{
    public NullException(string? message) : base(message) { }
}

[Serializable]

public class BlXMLFileLoadCreateException : Exception
{
    public BlXMLFileLoadCreateException(string? message) : base(message) { }
}
[Serializable]

public class BlArgumentException : Exception
{
    public BlArgumentException(string? message) : base(message) { }
}
[Serializable]

public class BlUnauthorizedOperationException : Exception
{
    public BlUnauthorizedOperationException(string? message) : base(message) { }
}

public class BlValidationException : Exception
{
    public BlValidationException(string? message) : base(message) { }
}

public class BlInvalidOperationException : Exception
{
    public BlInvalidOperationException(string? message) : base(message) { }
}

public class BlInvalidAddressException : Exception
{
    public BlInvalidAddressException(string? message) : base(message) { }
}

public class ApiRequestException : Exception
{
    public ApiRequestException(string? message) : base(message) { }
}

public class GeolocationNotFoundException : Exception
{
    public GeolocationNotFoundException(string? message) : base(message) { }
}




