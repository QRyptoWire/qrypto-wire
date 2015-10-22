using System.Collections.Generic;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
	public interface IQryptoWireServiceClient
	{
		string Login();
		void Register();
		void RegisterPushToken();
		IEnumerable<Contact> FetchContacts();
		IEnumerable<Message> FetchMessages();
		void AddContact(Contact contact);
		void SendMessage(Message message);
		int GetUserId();
	}
}
