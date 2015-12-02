using System.Windows;
using QRyptoWire.Core.Services;

namespace QRyptoWire.App.WPhone.Utilities
{
	public class PopupHelper : IPopupHelper
	{
		public void ShowRequestFailedPopup(string messageBody)
		{
			MessageBox.Show(messageBody, "Error",
				MessageBoxButton.OK);
		}

		public void ShowSuccessPopup(string messageBody)
		{
			MessageBox.Show(messageBody, "Success!",
				MessageBoxButton.OK);
		}
	}
}
