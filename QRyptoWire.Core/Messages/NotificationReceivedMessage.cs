using Cirrious.MvvmCross.Plugins.Messenger;

namespace QRyptoWire.Core.Messages
{
	public class NotificationReceivedMessage : MvxMessage
	{
		public NotificationReceivedMessage(object sender) : base(sender)
		{
		}
	}
}
