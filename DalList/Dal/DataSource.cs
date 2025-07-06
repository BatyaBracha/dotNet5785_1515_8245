namespace Dal;

/// <summary>
/// Provides in-memory data sources for DalList.
/// </summary>
public static class DataSource
{
    /// <summary>
    /// Gets the list of volunteers.
    /// </summary>
    /// <value>
    /// A list of <see cref="DO.Volunteer"/> objects.
    /// </value>
    internal static List<DO.Volunteer> Volunteers { get; } = new();

    /// <summary>
    /// Gets the list of calls.
    /// </summary>
    internal static List<DO.Call> Calls { get; } = new();

    /// <summary>
    /// Gets the list of assignments.
    /// </summary>
    internal static List<DO.Assignment> Assignments { get; } = new();
}
