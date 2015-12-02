using System;
using System.Collections.Generic;
using QRyptoWire.Core.Objects;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
	public interface IQryptoWireServiceClient
	{
		void SetDeviceId(string id);
		bool Login(string password);
		int Register(string password);
		void RegisterPushToken(string channelUri);
		IEnumerable<Contact> FetchContacts();
		IEnumerable<Message> FetchMessages();
		void AddContact(Contact contact);
		bool TrySendMessage(Message message);
		[Obsolete]
		int GetUserId();
		bool PushesAllowed();
		bool AllowPushes(bool allow);
	}
}
