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
		private string _deviceId;

		public void SetDeviceId(string id)
		{
			if(!string.IsNullOrWhiteSpace(_deviceId))
				throw new InvalidOperationException("Device id should be set only on startup!");
			_deviceId = id;
		}

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
			Execute(new RestRequest($"{ApiUris.Register}"));
		}

		public void RegisterPushToken(string channelUri)
		{
			Execute(new RestRequest($"{ApiUris.AddToken}{_sessionId}").AddBody(channelUri));
		}

		public IEnumerable<Contact> FetchContacts()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Message> FetchMessages()
		{
			throw new NotImplementedException();
		}

		public void AddContact(Contact contact)
		{
			throw new NotImplementedException();
		}

		public void SendMessage(Message message)
		{
			throw new NotImplementedException();
		}

		public int GetUserId()
		{
			throw new NotImplementedException();
		}
	}
}
