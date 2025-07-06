
using BO;

namespace BlApi;

/// <summary>
/// Interface for administrative operations in the BL layer.
/// </summary>
/// <summary>
/// Interface for administrative operations in the BL layer.
/// </summary>
/// <summary>
/// Interface for administrative operations in the business logic layer.
/// </summary>
public interface IAdmin 

{
    #region Stage 5
    /// <summary>
/// Adds an observer for configuration changes.
/// </summary>
/// <param name="configObserver">The action to invoke when config changes.</param>
void AddConfigObserver(Action configObserver);
    /// <summary>
/// Removes an observer for configuration changes.
/// </summary>
/// <param name="configObserver">The action to remove.</param>
void RemoveConfigObserver(Action configObserver);
    /// <summary>
/// Adds an observer for clock updates.
/// </summary>
/// <param name="clockObserver">The action to invoke when the clock updates.</param>
void AddClockObserver(Action clockObserver);
    /// <summary>
/// Removes an observer for clock updates.
/// </summary>
/// <param name="clockObserver">The action to remove.</param>
void RemoveClockObserver(Action clockObserver);
    #endregion Stage 5
    /// <summary>
/// Gets the current clock value.
/// </summary>
/// <returns>The current clock value.</returns>
DateTime GetClock();
    /// <summary>
/// Advances the clock by a specified time unit.
/// </summary>
/// <param name="timeUnit">The time unit to advance by.</param>
void PromotionClock(TimeUnit timeUnit);
    /// <summary>
/// Gets the current risk range.
/// </summary>
/// <returns>The current risk range.</returns>
TimeSpan GetRiskRange();
    /// <summary>
/// Sets the risk range.
/// </summary>
/// <param name="riskRange">The new risk range.</param>
void SetRiskRange(TimeSpan riskRange);
    /// <summary>
/// Resets the database to its initial state.
/// </summary>
void ResetDB();
    /// <summary>
/// Initializes the database.
/// </summary>
void InitializeDB();
    /// <summary>
/// Starts the simulator with the specified interval.
/// </summary>
/// <param name="interval">Interval for simulator updates.</param>
void StartSimulator(int interval); //stage 7
    /// <summary>
/// Stops the simulator.
/// </summary>
void StopSimulator(); //stage 7
}
