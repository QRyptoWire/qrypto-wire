using Cirrious.MvvmCross.Plugins.Messenger;

namespace QRyptoWire.Core.Messages
{
	public class RequestFailedMessage : MvxMessage
	{
		public RequestFailedMessage(object sender) : base(sender)
		{
		}
	}
}
