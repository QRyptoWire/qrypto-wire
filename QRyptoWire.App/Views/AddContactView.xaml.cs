using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

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

		protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
		{
			base.OnNavigatingFrom(e);
			var cancellationTask = Task.Run(async () => await Scanner.StopAsync());
			cancellationTask.Wait();
		}
	}
}
