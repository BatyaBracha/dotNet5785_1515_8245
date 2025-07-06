using System;
using System.Globalization;
using System.Windows.Data;

namespace PL.Volunteer
{
    /// <summary>
/// Converts update status to boolean true for UI binding.
/// </summary>
public class ConvertUpdateToTrue : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == "Update";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}