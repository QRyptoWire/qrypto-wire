using System;
using System.Globalization;
using System.Windows.Data;

namespace QRyptoWire.App.WPhone.Converters
{
	public class BoolToButtonTextConverter : IValueConverter
	{
	    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	    {
            var boolValue = (bool)value;
            if (boolValue)
                return "Register";
            return "Login";
        }

	    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	    {
	        throw new NotImplementedException();
	    }
	}
}
