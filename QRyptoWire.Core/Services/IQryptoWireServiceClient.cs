using System.Collections.Generic;
using QRyptoWire.Core.Objects;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
	public interface IQryptoWireServiceClient
	{
		void SetDeviceId(string id);
		bool Login(string password);
		void Register(string password);
		void RegisterPushToken(string channelUri);
		IEnumerable<Contact> FetchContacts();
		IEnumerable<Message> FetchMessages();
		void AddContact(QrContact contact);
		void SendMessage(Message message);
		int GetUserId();
		bool PushesAllowed();
		void AllowPushes(bool allow);
	}
}
