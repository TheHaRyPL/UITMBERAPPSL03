using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UITMBER.Enums;
using Xamarin.Forms;

namespace UITMBER.Views.Converters
{
    public class OrderStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            try
            { 
                if (value != null && OrderStatus.Finished.Equals((OrderStatus)value))
                    return "#941016";
            }
            catch (Exception)
            { }
            return "#bb171e";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
