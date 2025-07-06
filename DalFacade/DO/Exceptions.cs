namespace DO;

[Serializable]
/// <summary>
/// Exception thrown when an entity does not exist in DAL.
/// </summary>
public class DalDoesNotExistException : Exception
{
	public DalDoesNotExistException(string? message) : base(message) { }
}

[Serializable]

public class DalAlreadyExistsException : Exception
{
	public DalAlreadyExistsException(string? message) : base(message) { }
}

[Serializable]

public class DalDeletionImpossible : Exception
{
	public DalDeletionImpossible(string? message) : base(message) { }
}

[Serializable]

public class NullException : Exception
{
    public NullException(string? message) : base(message) { }
}

[Serializable]

public class DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}

public class DalUnauthorizedOperationException : Exception
{
    public DalUnauthorizedOperationException(string? message) : base(message) { }

}
