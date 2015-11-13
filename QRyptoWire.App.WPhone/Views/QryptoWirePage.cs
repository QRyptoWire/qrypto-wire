using Cirrious.MvvmCross.WindowsPhone.Views;
using Microsoft.Phone.Shell;

namespace QRyptoWire.App.WPhone.Views
{
	public abstract class QryptoWirePage : MvxPhonePage
	{
		protected QryptoWirePage()
		{
            Loaded += (sender, args) =>
            {
                SystemTray.IsVisible = false;
            };
        }
	}
}
