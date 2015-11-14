using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRyptoWire.Service.Core;
using QRyptoWire.Service.Data;

namespace QRyptoWire.Service.Core
{
	internal interface IContactService
	{
		IQueryable<Shared.Dto.Message> FetchContacts(string sessionKey);
		bool SendContact(string sessionKey, string msg);
	}
	
}
