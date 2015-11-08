using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace QRyptoWire.App.Views
{
	public sealed partial class AddContactView
	{
		public AddContactView()
		{
			InitializeComponent();
		}

		private async void ConfirmButtonTapped(object sender, TappedRoutedEventArgs e)
		{
			var btn = (Button) sender;
			await Scanner.StopAsync();
			btn.Command?.Execute(null);
		}
	}
}
