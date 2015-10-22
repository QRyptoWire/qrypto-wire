using System.Collections.Generic;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
	public class QryptoWireServiceClient : IQryptoWireServiceClient
	{
		private string _sessionId;

		public string Login()
		{
			throw new System.NotImplementedException();
		}

		public void Register()
		{
			throw new System.NotImplementedException();
		}

		public void RegisterPushToken()
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<Contact> FetchContacts()
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<Message> FetchMessages()
		{
			throw new System.NotImplementedException();
		}

		public void AddContact(Contact contact)
		{
			throw new System.NotImplementedException();
		}

		public void SendMessage(Message message)
		{
			throw new System.NotImplementedException();
		}

		public int GetUserId()
		{
			throw new System.NotImplementedException();
		}
	}
}
