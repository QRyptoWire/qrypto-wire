using System.Windows.Navigation;

namespace QRyptoWire.App.WPhone.Views
{
	public sealed partial class AddContactView
	{
		public AddContactView()
		{
			InitializeComponent();
		}

        //private async void ConfirmButtonTapped(object sender, TappedRoutedEventArgs e)
        //{
        //	await Scanner.StopAsync();
        //}

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            //await Scanner.StopAsync();
            Scanner.Stop();
        }
    }
}
