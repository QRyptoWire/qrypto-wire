using System.Collections.Generic;
using QRyptoWire.Core.ViewModels;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
	public interface IStorageService
	{
		bool PublicKeyExists();
		void ClearMessages();
		IEnumerable<string> GetMessages(int userId);
		IEnumerable<ContactListItem> GetContacts();
		void SaveMessage(Message message);
	}
}
