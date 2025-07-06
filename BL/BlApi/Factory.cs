namespace BlApi;
/// <summary>
/// Provides factory methods for creating BL interfaces.
/// </summary>
public static class Factory
{
    /// <summary>
/// Gets an instance of the main BL interface.
/// </summary>
/// <returns>An instance of <see cref="IBl"/>.</returns>
    /// <summary>
    /// Gets an instance of the business logic layer (IBl).
    /// </summary>
    /// <returns>An instance of IBl.</returns>
    public static IBl Get() => new BlImplementation.Bl();

}

