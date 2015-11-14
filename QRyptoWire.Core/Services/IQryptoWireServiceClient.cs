using System.Collections.Generic;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
	public interface IQryptoWireServiceClient
	{
		void SetDeviceId(string id);
		bool Login(string deviceId, string password);
		void Register(string deviceId, string password);
		void RegisterPushToken(string channelUri);
		IEnumerable<Contact> FetchContacts();
		IEnumerable<Message> FetchMessages();
		void AddContact(Contact contact);
		void SendMessage(Message message);
		int GetUserId();
	}
}
