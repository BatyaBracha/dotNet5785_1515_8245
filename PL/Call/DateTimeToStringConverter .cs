using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
namespace PL.Call
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dt)
                return dt.ToString("o"); // פורמט ISO 8601 עם זמן ואזור
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateTime.TryParse((string)value, null, DateTimeStyles.RoundtripKind, out var result))
                return result;

            return DependencyProperty.UnsetValue;
        }
    }
}
