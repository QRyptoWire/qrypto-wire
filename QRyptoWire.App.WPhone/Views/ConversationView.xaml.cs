namespace QRyptoWire.App.WPhone.Views
{
	public partial class ConversationView
	{
		public ConversationView()
		{
			InitializeComponent();
			Scroll.SizeChanged += (sender, args) =>
			{
				Scroll.UpdateLayout();
				Scroll.ScrollToVerticalOffset(Scroll.ScrollableHeight);
			};
		}
	}
}