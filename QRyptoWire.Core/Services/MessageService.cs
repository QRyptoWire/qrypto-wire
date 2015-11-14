using System.Collections.Generic;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
	public class MessageService : IMessageService
	{
		private readonly IStorageService _storageService;
		private readonly IQryptoWireServiceClient _client;

		public MessageService(IStorageService storageService, IQryptoWireServiceClient client)
		{
			_storageService = storageService;
			_client = client;
		}

		public void SendMessage(Message message)
		{
			_storageService.SaveMessage(message);
			_client.SendMessage(message);
		}

		public IEnumerable<Message> FetchMessages()
		{
			throw new System.NotImplementedException();
		}
	}
}
