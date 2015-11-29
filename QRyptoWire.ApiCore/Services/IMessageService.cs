using System.Collections.Generic;
using System.Linq;

namespace QRyptoWire.Service.Core
{
	internal interface IMessageService
	{
		List<Shared.Dto.Message> FetchMessages(string sessionKey);
		bool SendMessage(string sessionKey, Shared.Dto.Message msg);
	}
	
}
