using MSI.Keyboard.Backlight.Manager.Jobs.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace MSI.Keyboard.Backlight.Manager.UI.Converters
{
    public class BacklightJobToCheckedConverter : IValueConverter
    {
        public BacklightJobType Mode { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((BacklightJobType)value) == Mode;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
