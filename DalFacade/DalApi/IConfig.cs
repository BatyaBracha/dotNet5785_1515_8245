
namespace DalApi;

/// <summary>
/// Defines configuration methods for the DAL layer.
/// </summary>
public interface IConfig
{
    TimeSpan RiskRange { get; set; }
    DateTime Clock { get; set; }
    void Reset();

}

