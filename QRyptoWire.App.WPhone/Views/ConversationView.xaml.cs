using System.Windows;

namespace QRyptoWire.App.WPhone.Views
{
	public partial class ConversationView
	{
		public ConversationView()
		{
			InitializeComponent();
			SizeChangedEventHandler scrollToBottomHandler = null;
			scrollToBottomHandler = delegate
			{
				Scroll.UpdateLayout();
				Scroll.ScrollToVerticalOffset(Scroll.ScrollableHeight);
				Scroll.SizeChanged -= scrollToBottomHandler;
			};
			Scroll.SizeChanged += scrollToBottomHandler;
		}
	}
}