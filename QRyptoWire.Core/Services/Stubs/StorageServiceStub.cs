using System.Collections.Generic;
using QRyptoWire.Core.ViewModels;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services.Stubs
{
	public class StorageServiceStub : IStorageService
	{
		public bool PublicKeyExists()
		{
			return true;
		}

		public void ClearMessages()
		{
		}

		public IEnumerable<string> GetMessages(int userId)
		{
			return new[] {"hello", "long messasge to test how view will behave hope it wont break"};
		}

		public IEnumerable<ContactListItem> GetContacts()
		{
			return new[] {new ContactListItem {Id = 0, Name = "top kek", UnreadMessages = 1}};
		}

		public void SaveMessage(Message message)
		{
		}
	}
}
