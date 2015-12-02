using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Cirrious.MvvmCross.Plugins.Messenger;
using QRyptoWire.Shared;
using QRyptoWire.Shared.Dto;
using RestSharp.Portable;

namespace QRyptoWire.Core.Services
{
	public class QryptoWireServiceClient : ApiClientBase, IQryptoWireServiceClient
	{
		private string _sessionId;
		private string _deviceId;

		public QryptoWireServiceClient(IMvxMessenger messenger) : base(messenger)
		{
		}

		public void SetDeviceId(string id)
		{
			_deviceId = id;
		}

		public bool Login(string password)
		{
			var request = new RestRequest(ApiUris.Login, HttpMethod.Post);
			request.AddParameter("DeviceId", _deviceId);
            request.AddParameter("Password", password);
			_sessionId = Execute<string>(request);
			return _sessionId != null;
		}

		public int Register(string password)
		{
			var request = new RestRequest("api/Register", HttpMethod.Post);
			request.AddParameter("DeviceId", _deviceId);
			request.AddParameter("Password", password);
			return Execute<int>(request);
		}

		public void RegisterPushToken(string channelUri)
		{
			Execute(new RestRequest($"{ApiUris.AddToken}{_sessionId}").AddJsonBody(channelUri));
		}

		public IEnumerable<Contact> FetchContacts()
		{
			var res = Execute<IEnumerable<Contact>>(new RestRequest($"{ApiUris.FetchContacts}{_sessionId}", HttpMethod.Get));
			return res ?? Enumerable.Empty<Contact>();
		}

		public IEnumerable<Message> FetchMessages()
		{
			var res = Execute<IEnumerable<Message>>(new RestRequest($"{ApiUris.FetchMessages}{_sessionId}", HttpMethod.Get));
			return res ?? Enumerable.Empty<Message>();
		}

		public void AddContact(Contact contact)
		{
			Execute(
				new RestRequest(
					$"{ApiUris.AddContact}{_sessionId}",
					HttpMethod.Post
					)
					.AddJsonBody(contact)
				);
		}

		public void SendMessage(Message message)
		{
			Execute(
				new RestRequest(
					$"{ApiUris.SendMessage}{_sessionId}", 
					HttpMethod.Post
				)
				.AddJsonBody(message)
			);
		}

		public int GetUserId()
		{
			return Execute<int>(new RestRequest($"{ApiUris.GetUserId}{_sessionId}"));
		}

		public bool PushesAllowed()
		{
			return Execute<bool>(new RestRequest($"{ApiUris.GetPushesAllowed}{_sessionId}"));
		}

		public bool AllowPushes(bool allow)
		{
			return TryExecute(new RestRequest($"{ApiUris.AllowPushes}{_sessionId}").AddBody(allow));
		}
	}
}
