//using System;
//using System.Globalization;
//using System.Windows;
//using System.Windows.Data;

//namespace PL.Volunteer
//{
//    public class ConvertUpdateToVisible : IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            return value?.ToString() == "Update" ? Visibility.Visible : Visibility.Collapsed;
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PL.Volunteer
{
    public class ConvertUpdateToVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == "Update" ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
