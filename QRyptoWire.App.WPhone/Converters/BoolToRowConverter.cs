using System;
using System.Globalization;
using System.Windows.Data;

namespace QRyptoWire.App.WPhone.Converters
{
	public class BoolToRowConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var boolValue = (bool) value;
			return boolValue ? 1 : 0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
