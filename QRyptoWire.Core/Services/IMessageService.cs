using QRyptoWire.Core.Objects;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
	public interface IMessageService
	{
		bool TrySendMessage(Message message);
		bool FetchMessages();
		void AddContact(QrContact contact);
		bool FetchContacts();
	}
}
