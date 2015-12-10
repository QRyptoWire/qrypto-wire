using System;
using System.Globalization;
using System.Windows.Data;

namespace QRyptoWire.App.WPhone.Converters
{
	public class UnreadMessagesToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var intValue = (int) value;
			return intValue == 0 ? "No new messages" : "Unread messages: " + intValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
