using DalApi;

namespace Dal;

/// <summary>
/// Implements configuration logic for the DAL List layer.
/// </summary>
internal class ConfigImplementation : IConfig
{
    /// <summary>
    /// Gets or sets the current clock time.
    /// </summary>
    /// <value>The current clock time.</value>
    public DateTime Clock { 
        /// <summary>
        /// Gets the current clock time.
        /// </summary>
        /// <returns>The current clock time.</returns>
        get => Config.Clock;
        /// <summary>
        /// Sets the current clock time.
        /// </summary>
        /// <param name="value">The new clock time.</param>
        set => Config.Clock = value;
    }
    /// <summary>
    /// Gets or sets the risk range time span.
    /// </summary>
    public TimeSpan RiskRange { get; set; }
    //...
    /// <summary>
    /// Resets the configuration to its default state.
    /// </summary>
    public void Reset()
    {
        Config.Reset();
    }

}
