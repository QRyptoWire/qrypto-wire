using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace QRyptoWire.App.WPhone.Converters
{
	public class BoolToColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((bool) value)
				return new SolidColorBrush(Colors.Orange);
			else
				return new SolidColorBrush(Colors.Red);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
