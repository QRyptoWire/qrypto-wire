using System.Collections.Generic;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
	public interface IContactsService
	{
		void AddContact(Contact contact);
		IEnumerable<Contact> FetchContacts();
	}
}
