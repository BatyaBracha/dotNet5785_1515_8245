
namespace BO;

[Serializable]
/// <summary>
/// Exception thrown when an entity does not exist in BL.
/// </summary>
public class BlDoesNotExistException : Exception
{
    /// <summary>
/// Initializes a new instance of the <see cref="BlDoesNotExistException"/> class with a message.
/// </summary>
/// <param name="message">The exception message.</param>
public BlDoesNotExistException(string? message) : base(message) { }
    /// <summary>
/// Initializes a new instance of the <see cref="BlDoesNotExistException"/> class with a message and inner exception.
/// </summary>
/// <param name="message">The exception message.</param>
/// <param name="innerException">The inner exception.</param>
public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
/// <summary>
/// Exception thrown when a required property is null in BL.
/// </summary>
public class BlNullPropertyException : Exception
{
    /// <summary>
/// Initializes a new instance of the <see cref="BlNullPropertyException"/> class with a message and property name.
/// </summary>
/// <param name="message">The exception message.</param>
/// <param name="propertyName">The name of the property that is null.</param>
public BlNullPropertyException(string? message, string? propertyName) : base($"{message} (Property: {propertyName})") { }
}


[Serializable]

/// <summary>
/// Exception thrown when an entity already exists in BL.
/// </summary>
public class BlAlreadyExistsException : Exception
{
    /// <summary>
/// Initializes a new instance of the <see cref="BlAlreadyExistsException"/> class with a message.
/// </summary>
/// <param name="message">The exception message.</param>
public BlAlreadyExistsException(string? message) : base(message) { }
}

[Serializable]

/// <summary>
/// Exception thrown when deletion is impossible in BL.
/// </summary>
public class BlDeletionImpossible : Exception
{
    /// <summary>
/// Initializes a new instance of the <see cref="BlDeletionImpossible"/> class with a message.
/// </summary>
/// <param name="message">The exception message.</param>
public BlDeletionImpossible(string? message) : base(message) { }
}

[Serializable]

/// <summary>
/// Exception thrown when a null reference is encountered in BL.
/// </summary>
public class NullException : Exception
{
    /// <summary>
/// Initializes a new instance of the <see cref="NullException"/> class with a message.
/// </summary>
/// <param name="message">The exception message.</param>
public NullException(string? message) : base(message) { }
}

[Serializable]

/// <summary>
/// Exception thrown when loading or creating an XML file fails in BL.
/// </summary>
public class BlXMLFileLoadCreateException : Exception
{
    /// <summary>
/// Initializes a new instance of the <see cref="BlXMLFileLoadCreateException"/> class with a message.
/// </summary>
/// <param name="message">The exception message.</param>
public BlXMLFileLoadCreateException(string? message) : base(message) { }
}
[Serializable]

/// <summary>
/// Exception thrown for invalid arguments in BL.
/// </summary>
public class BlArgumentException : Exception
{
    /// <summary>
/// Initializes a new instance of the <see cref="BlArgumentException"/> class with a message.
/// </summary>
/// <param name="message">The exception message.</param>
public BlArgumentException(string? message) : base(message) { }
}
[Serializable]

/// <summary>
/// Exception thrown for unauthorized operations in BL.
/// </summary>
public class BlUnauthorizedOperationException : Exception
{
    /// <summary>
/// Initializes a new instance of the <see cref="BlUnauthorizedOperationException"/> class with a message.
/// </summary>
/// <param name="message">The exception message.</param>
public BlUnauthorizedOperationException(string? message) : base(message) { }
}

/// <summary>
/// Exception thrown for validation errors in BL.
/// </summary>
public class BlValidationException : Exception
{
    /// <summary>
/// Initializes a new instance of the <see cref="BlValidationException"/> class with a message.
/// </summary>
/// <param name="message">The exception message.</param>
public BlValidationException(string? message) : base(message) { }
}

/// <summary>
/// Exception thrown for invalid operations in BL.
/// </summary>
public class BlInvalidOperationException : Exception
{
    /// <summary>
/// Initializes a new instance of the <see cref="BlInvalidOperationException"/> class with a message.
/// </summary>
/// <param name="message">The exception message.</param>
public BlInvalidOperationException(string? message) : base(message) { }
}

/// <summary>
/// Exception thrown for invalid addresses in BL.
/// </summary>
public class BlInvalidAddressException : Exception
{
    /// <summary>
/// Initializes a new instance of the <see cref="BlInvalidAddressException"/> class with a message.
/// </summary>
/// <param name="message">The exception message.</param>
public BlInvalidAddressException(string? message) : base(message) { }
}

/// <summary>
/// Exception thrown for API request errors in BL.
/// </summary>
public class ApiRequestException : Exception
{
    /// <summary>
/// Initializes a new instance of the <see cref="ApiRequestException"/> class with a message.
/// </summary>
/// <param name="message">The exception message.</param>
public ApiRequestException(string? message) : base(message) { }
}

/// <summary>
/// Exception thrown when geolocation information is not found in BL.
/// </summary>
public class GeolocationNotFoundException : Exception
{
    /// <summary>
/// Initializes a new instance of the <see cref="GeolocationNotFoundException"/> class with a message.
/// </summary>
/// <param name="message">The exception message.</param>
public GeolocationNotFoundException(string? message) : base(message) { }
}




