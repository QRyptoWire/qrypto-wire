using System;
using System.Collections.Generic;
using QRyptoWire.Shared;
using QRyptoWire.Shared.Dto;
using RestSharp.Portable;

namespace QRyptoWire.Core.Services
{
	public class QryptoWireServiceClient : ApiClientBase, IQryptoWireServiceClient
	{
		private string _sessionId;

		public bool Login(string deviceId, string password)
		{
			var request = new RestRequest($"{ApiUris.Login}{deviceId}/{password}/");
			_sessionId = Execute<string>(request);
			if (_sessionId == null)
				return false;
			return true;
		}

		public void Register(string deviceId, string password)
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
