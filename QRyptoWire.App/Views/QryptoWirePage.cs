using System;
using Windows.UI.ViewManagement;
using Cirrious.MvvmCross.WindowsCommon.Views;

namespace QRyptoWire.App.Views
{
	public abstract class QryptoWirePage : MvxWindowsPage
	{
		protected QryptoWirePage()
		{
			Loaded += async (sender, args) =>
			{
				await StatusBar.GetForCurrentView().HideAsync();
			};
		}
	}
}
