using Cirrious.MvvmCross.Plugins.Messenger;

namespace QRyptoWire.Core.Messages
{
	public class ContentReceivedMessage : MvxMessage
	{
		public ContentReceivedMessage(object sender) : base(sender)
		{
		}
	}
}
