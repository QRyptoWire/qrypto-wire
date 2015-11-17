using System.Linq;

namespace QRyptoWire.Service.Core
{
	internal interface IContactService
	{
		IQueryable<Shared.Dto.Message> FetchContacts(string sessionKey);
		bool SendContact(string sessionKey, int recipientId, string msg);
	}
	
}
