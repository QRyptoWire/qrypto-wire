using System.Collections.Generic;
using System.Linq;
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
			_storageService.SaveMessages(new[] {message});
			_client.SendMessage(message);
		}

		public IEnumerable<Message> FetchMessages()
		{
			var messages = _client.FetchMessages().ToList();
			if(messages.Any())
				_storageService.SaveMessages(messages);
			return messages;
		}

		public void FetchContacts()
		{
			var contacts = _client.FetchContacts().ToList();
			if(contacts.Any())
				_storageService.SaveContacts(contacts);
		}
	}
}
