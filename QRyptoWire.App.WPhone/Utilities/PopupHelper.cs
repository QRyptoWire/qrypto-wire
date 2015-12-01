using System.Windows;
using QRyptoWire.Core.Services;

namespace QRyptoWire.App.WPhone.Utilities
{
	public class PopupHelper : IPopupHelper
	{
		public void ShowRequestFailedPopup()
		{
			MessageBox.Show("Request to service failed. Make sure you have a stable internet connection", "Error",
				MessageBoxButton.OK);
		}

		public void ShowSuccessPopup(string messageBody)
		{
			MessageBox.Show(messageBody, "Success!",
				MessageBoxButton.OK);
		}
	}
}
