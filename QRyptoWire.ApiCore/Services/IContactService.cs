using System.Collections.Generic;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Service.Core
{
	internal interface IContactService
	{
		List<Contact> FetchContacts(string sessionKey);
		bool SendContact(string sessionKey, Contact contact);
	}
	
}
