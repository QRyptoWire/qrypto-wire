using System.Linq;

namespace QRyptoWire.Service.Core
{
	internal interface IMessageService
	{
		IQueryable<Shared.Dto.Message> FetchMessages(string sessionKey);
		bool SendMessage(string sessionKey, Shared.Dto.Message msg);
	}
	
}
