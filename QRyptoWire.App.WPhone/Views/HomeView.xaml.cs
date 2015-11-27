using System.Windows.Navigation;

namespace QRyptoWire.App.WPhone.Views
{
	public sealed partial class HomeView
	{
		public HomeView()
		{
			InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			ContactList.SelectedIndex = -1;
		}
	}
}
