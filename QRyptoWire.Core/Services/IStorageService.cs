using System.Collections.Generic;
using QRyptoWire.Core.ViewModels;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
	public interface IStorageService
	{
		bool PublicKeyExists();
        string GetPublicKey();
	    int GetUserId();
		void ClearMessages();
		IEnumerable<StoredMessage> GetMessages(int userId);
		IEnumerable<ContactListItem> GetContacts();
		void SaveMessages(IEnumerable<Message> messages);
		void SaveContacts(IEnumerable<Contact> contacts);
	}
}
