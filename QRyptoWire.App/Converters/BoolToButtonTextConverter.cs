using System;
using Windows.UI.Xaml.Data;

namespace QRyptoWire.App.Converters
{
	public class BoolToButtonTextConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var boolValue = (bool)value;
			if (boolValue)
				return "Register";
			return "Login";
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
