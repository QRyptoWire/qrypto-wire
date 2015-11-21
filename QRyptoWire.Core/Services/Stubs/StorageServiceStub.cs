using System;
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

	    public string GetPublicKey()
	    {
	        return string.Empty;
	    }

	    public int GetUserId()
	    {
	        return 0;
	    }

	    public void ClearMessages()
		{
		}

		public IEnumerable<StoredMessage> GetMessages(int userId)
		{
			return new[]
			{
				new StoredMessage { Body = "hello", Date = DateTime.MinValue, Sent = false},
				new StoredMessage
				{
					Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
					Date = DateTime.Now, Sent = true
				},
				new StoredMessage {Body = "top kek bro" ,Date = DateTime.Now, Sent = false}
			};
		}

		public IEnumerable<ContactListItem> GetContacts()
		{
			return new[] {new ContactListItem {Id = 0, Name = "top kek", UnreadMessages = 1}};
		}

		public void SaveMessages(IEnumerable<Message> message)
		{
		}

		public void SaveContacts(IEnumerable<Contact> contacts)
		{
		}
	}
}
