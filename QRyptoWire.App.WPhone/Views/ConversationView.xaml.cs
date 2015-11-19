namespace QRyptoWire.App.WPhone.Views
{
	public partial class ConversationView
	{
		public ConversationView()
		{
			InitializeComponent();
			ScrollViewer.UpdateLayout();
			ScrollViewer.ScrollToHorizontalOffset(double.MaxValue);
		}
	}
}