using Cirrious.MvvmCross.Plugins.Messenger;

namespace QRyptoWire.Core.Messages
{
	public class RequestFailedMessage : MvxMessage
	{
		public string MessageBody { get; set; }

		public RequestFailedMessage(object sender, string messageBody) : base(sender)
		{
			MessageBody = messageBody;
		}
	}
}
