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
	    private readonly IEncryptionService _encryptionService;

	    public MessageService(IStorageService storageService, IQryptoWireServiceClient client, IEncryptionService encryptionService)
		{
			_storageService = storageService;
			_client = client;
	        _encryptionService = encryptionService;
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

            var encMsg = _encryptionService.Encrypt(message.Body, message.ReceiverId);
            _client.SendMessage(new Message
            {
                Body = encMsg.Body,
                DateSent = message.DateSent,
                Iv = encMsg.Iv,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                SymmetricKey = encMsg.SymmetricKey,
                DigitalSignature = encMsg.DigitalSignature
            });
        }

		public void FetchMessages()
		{
			var messages = _client.FetchMessages().ToList();
		    var verfiedMessages = new List<MessageItem>();

		    foreach (var msg in messages)
            {
                string messageText;

                if (_encryptionService.Decrypt(
                    new EncryptedMessage {Body = msg.Body, DigitalSignature = msg.DigitalSignature, Iv = msg.Iv, SymmetricKey = msg.SymmetricKey},
                    msg.SenderId, 
                    out messageText))
                {
                    verfiedMessages.Add(new MessageItem
                    {
                        Body = messageText,
                        Date = msg.DateSent,
                        ReceiverId = msg.ReceiverId,
                        SenderId = msg.SenderId,
                        IsNew = true
                    });
                }
            }

            _storageService.SaveMessages(verfiedMessages);
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

		    _client.AddContact(new Contact
		    {
                Name = contact.Name,
                PublicKey = _encryptionService.ExtractPublicKey(_storageService.GetUser().KeyPair),
                ReceiverId = contact.UserId,
                SenderId = _storageService.GetUser().Id
		    });
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
