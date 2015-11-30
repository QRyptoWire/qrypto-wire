using System.Collections.Generic;
using System.Linq;
using QRyptoWire.Core.DbItems;
using QRyptoWire.Core.Objects;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services.Implementation
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
			_storageService.SaveMessages(new[] {new MessageItem
			{
				Body = message.Body,
				Date = message.DateSent,
				ReceiverId = message.ReceiverId,
				SenderId = message.SenderId
			} });
			_client.SendMessage(message);
		}

		public IEnumerable<Message> FetchMessages()
		{
			var messages = _client.FetchMessages().ToList();
			if(messages.Any())
				_storageService.SaveMessages(messages.Select(e => new MessageItem
				{
					Body = e.Body,
					Date = e.DateSent,
					ReceiverId = e.ReceiverId,
					SenderId = e.SenderId,
					IsNew = true
				}).ToList());
			return messages;
		}

		public void AddContact(QrContact contact)
		{
			_storageService.SaveContacts(new[] { new ContactItem
			{
				Id = contact.UserId,
				PublicKey = contact.PublicKey,
				IsNew = true,
				Name = contact.Name
			} });
			_client.AddContact(contact);
		}

		public void FetchContacts()
		{
			var contacts = _client.FetchContacts().ToList();
			if(contacts.Any())
				_storageService.SaveContacts(contacts.Select(e => new ContactItem
				{
					Id = e.SenderId,
					IsNew = true,
					Name = e.Name,
					PublicKey = e.PublicKey
				}).ToList());
		}
	}
}
