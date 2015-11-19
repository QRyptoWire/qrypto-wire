using System.Collections.Generic;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
	public interface IMessageService
	{
		void SendMessage(Message message);
		IEnumerable<Message> FetchMessages();
		void FetchContacts();
	}
}
