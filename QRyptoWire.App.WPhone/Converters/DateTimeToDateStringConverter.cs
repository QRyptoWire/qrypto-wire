using System;
using System.Globalization;
using System.Windows.Data;

namespace QRyptoWire.App.WPhone.Converters
{
	public class DateTimeToDateStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((DateTime) value).ToShortDateString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
