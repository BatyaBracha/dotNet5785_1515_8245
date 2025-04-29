
using BO;

namespace BlApi;

public interface IAdmin 

{
    #region Stage 5
    void AddConfigObserver(Action configObserver);
    void RemoveConfigObserver(Action configObserver);
    void AddClockObserver(Action clockObserver);
    void RemoveClockObserver(Action clockObserver);
    #endregion Stage 5
    DateTime GetClock();
    void PromotionClock(TimeUnit timeUnit);
    TimeSpan GetRiskRange();
    void SetRiskRange(TimeSpan riskRange);
    void ResetDB();
    void InitializeDB();
}
