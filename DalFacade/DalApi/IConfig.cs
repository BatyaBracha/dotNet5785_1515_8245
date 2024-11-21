
namespace DalApi;

public interface IConfig
{
    TimeSpan RiskRange();
    DateTime Clock { get; set; }
    void Reset();

}

