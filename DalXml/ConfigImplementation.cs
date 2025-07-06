namespace Dal;
using DalApi;
using System.Reflection.Metadata.Ecma335;

/// <summary>
/// Implements configuration logic for the DAL XML layer.
/// </summary>
internal class ConfigImplementation : IConfig
{
    /// <summary>
    /// Gets or sets the current clock value.
    /// </summary>
        /// <summary>
    /// Gets or sets the current clock value.
    /// </summary>
    public DateTime Clock
    {
        get => Config.Clock;
        set => Config.Clock = value;
    }
    //...
        /// <summary>
    /// Resets the configuration to default values.
    /// </summary>
    public void Reset()
    {
        Config.Reset();
    }
    //public DateTime RiskRange
    //{
    //    get => Config.RiskRange;
    //    set => Config.RiskRange = value;
    //}
        /// <summary>
    /// Gets or sets the risk range time span.
    /// </summary>
    public TimeSpan RiskRange
    {
        get => Config.RiskRange;
        set => Config.RiskRange = value;
    }
}
