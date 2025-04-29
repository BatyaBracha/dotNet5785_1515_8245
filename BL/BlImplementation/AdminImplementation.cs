using BlApi;
using BO;
using DalApi;
using DalTest;
using Helpers;

namespace BlImplementation
{
    internal class AdminImplementation : IAdmin
    {

        private readonly DalApi.IDal Admin_dal = DalApi.Factory.Get;

        #region Stage 52
        public void AddClockObserver(Action clockObserver) =>
        AdminManager.ClockUpdatedObservers += clockObserver;
        public void RemoveClockObserver(Action clockObserver) =>
        AdminManager.ClockUpdatedObservers -= clockObserver;
        public void AddConfigObserver(Action configObserver) =>
       AdminManager.ConfigUpdatedObservers += configObserver;
        public void RemoveConfigObserver(Action configObserver) =>
        AdminManager.ConfigUpdatedObservers -= configObserver;
        #endregion Stage 5

        // מתודת בקשת שעון
        public DateTime Clock()
        {
            // מחזיר את הזמן הנוכחי מהשעון המערכת
            return AdminManager.Now;
        }

        // מתודת קידום שעון
        public void PromotionClock(BO.TimeUnit timeUnit)
        {
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
        public TimeSpan GetRiskRange()
        {
            // מחזיר את טווח הזמן של הסיכון שנשמר בתצורה
            return AdminManager.RiskRange;
        }

        // מתודת הגדרת טווח זמן סיכון
        public void SetRiskRange(TimeSpan riskRange)
        {
            // מעדכן את טווח הזמן של הסיכון בתצורה
            AdminManager.RiskRange = riskRange;
        }

        // מתודת איפוס בסיס נתונים
        public void ResetDB()
        {
            AdminManager.ResetDB();
            Admin_dal.Volunteer.DeleteAll();
            Admin_dal.Assignment.DeleteAll();
            Admin_dal.Call.DeleteAll();
        }

        // מתודת אתחול בסיס נתונים
        public void InitializeDB()
        {
            // אתחול של בסיס הנתונים: איפוס וטעינה מחדש של הנתונים
            ResetDB();
            // אתחול הישויות עם ערכים התחלתיים
            Initialization.Do();
        }
    }
}