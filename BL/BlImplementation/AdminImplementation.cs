using BlApi;
using BO;
using DalApi;
using DalTest;
using Helpers;


namespace BlImplementation
{
    /// <summary>
/// Implementation of administrative operations in the BL layer.
/// </summary>
/// <summary>
/// Implements administrative business logic operations.
/// </summary>
internal class AdminImplementation : IAdmin
    {

        private readonly DalApi.IDal Admin_dal = DalApi.Factory.Get;

        #region Stage 5
        /// <summary>
/// Adds an observer for clock updates.
/// </summary>
/// <param name="clockObserver">The action to invoke when the clock updates.</param>
public void AddClockObserver(Action clockObserver) =>
        AdminManager.ClockUpdatedObservers += clockObserver;
        /// <summary>
/// Removes an observer for clock updates.
/// </summary>
/// <param name="clockObserver">The action to remove.</param>
public void RemoveClockObserver(Action clockObserver) =>
        AdminManager.ClockUpdatedObservers -= clockObserver;
        /// <summary>
/// Adds an observer for configuration changes.
/// </summary>
/// <param name="configObserver">The action to invoke when config changes.</param>
public void AddConfigObserver(Action configObserver) =>
       AdminManager.ConfigUpdatedObservers += configObserver;
        /// <summary>
/// Removes an observer for configuration changes.
/// </summary>
/// <param name="configObserver">The action to remove.</param>
public void RemoveConfigObserver(Action configObserver) =>
        AdminManager.ConfigUpdatedObservers -= configObserver;
        #endregion Stage 5

        // מתודת בקשת שעון
        /// <summary>
/// Gets the current clock value.
/// </summary>
/// <returns>The current clock value.</returns>
public DateTime GetClock()
        {
            // מחזיר את הזמן הנוכחי מהשעון המערכת
            return AdminManager.Now;
        }

        // מתודת קידום שעון
        /// <summary>
/// Advances the clock by a specified time unit.
/// </summary>
/// <param name="timeUnit">The time unit to advance by.</param>
public void PromotionClock(BO.TimeUnit timeUnit)
        {
            AdminManager.ThrowOnSimulatorIsRunning();
            DateTime newTime;

            // קידום השעון בהתאם ליחידת הזמן
            switch (timeUnit)
            {
                case BO.TimeUnit.MINUTE:
                    AdminManager.UpdateClock(AdminManager.Now.AddMinutes(1));
                    break;
                case BO.TimeUnit.HOUR:
                    AdminManager.UpdateClock(AdminManager.Now.AddHours(1));
                    break;
                case BO.TimeUnit.DAY:
                    AdminManager.UpdateClock(AdminManager.Now.AddDays(1));
                    break;
                case BO.TimeUnit.MONTH:
                    AdminManager.UpdateClock(AdminManager.Now.AddMonths(1));
                    break;
                case BO.TimeUnit.YEAR:
                    AdminManager.UpdateClock(AdminManager.Now.AddYears(1));
                    break;
                default:
                    throw new BlArgumentException("Unknown time unit");
            }
        }

        // מתודת בקשת טווח זמן סיכון
        /// <summary>
/// Gets the current risk range.
/// </summary>
/// <returns>The current risk range.</returns>
public TimeSpan GetRiskRange()
        {
            // מחזיר את טווח הזמן של הסיכון שנשמר בתצורה
            return AdminManager.RiskRange;
        }

        // מתודת הגדרת טווח זמן סיכון
        /// <summary>
/// Sets the risk range.
/// </summary>
/// <param name="riskRange">The new risk range.</param>
public void SetRiskRange(TimeSpan riskRange)
        {
            AdminManager.ThrowOnSimulatorIsRunning();
            // מעדכן את טווח הזמן של הסיכון בתצורה
            AdminManager.RiskRange = riskRange;
        }

        // מתודת איפוס בסיס נתונים
        //public void ResetDB()
        //{
        //    AdminManager.ResetDB();
        //    Admin_dal.Volunteer.DeleteAll();
        //    Admin_dal.Assignment.DeleteAll();
        //    Admin_dal.Call.DeleteAll();
        //}
        /// <summary>
/// Resets the database to its initial state.
/// </summary>
public void ResetDB() //stage 4
        {
            AdminManager.ThrowOnSimulatorIsRunning();  //stage 7
            AdminManager.ResetDB(); //stage 7
        }

        // מתודת אתחול בסיס נתונים
        //public void InitializeDB()
        //{
        //    // אתחול של בסיס הנתונים: איפוס וטעינה מחדש של הנתונים
        //    ResetDB();
        //    // אתחול הישויות עם ערכים התחלתיים
        //    Initialization.Do();
        //}
        /// <summary>
/// Initializes the database.
/// </summary>
public void InitializeDB() //stage 4
        {
            AdminManager.ThrowOnSimulatorIsRunning();  //stage 7
            AdminManager.InitializeDB(); //stage 7
        }


        /// <summary>
/// Starts the simulator with the specified interval.
/// </summary>
/// <param name="interval">Interval for simulator updates.</param>
public void StartSimulator(int interval)  //stage 7
        {
            AdminManager.ThrowOnSimulatorIsRunning();  //stage 7
            AdminManager.Start(interval); //stage 7
        }
        /// <summary>
/// Stops the simulator.
/// </summary>
public void StopSimulator()
            => AdminManager.Stop(); //stage 7

    }


}