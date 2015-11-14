using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRyptoWire.Service.Core;
using QRyptoWire.Service.Data;

namespace QRyptoWire.Service.Core
{
	public class MessageService : IMessageService
	{
		public IQueryable<Shared.Dto.Message> FetchMessages(string sessionKey)
		{
			//TODO: delete
			using (var dbContext = new DataModel())
			{
				var sessionService = new SessionService(dbContext);
				var user = sessionService.GetUser(sessionKey);
				if (user != null)
				{
					var messages = dbContext.Messages
					.Where(m => m.RecipientId == user.Id)
					.Select(e => new Shared.Dto.Message
					{
						Body = e.Content,
						ReceiverId = e.RecipientId,
						SenderId = e.SenderId
					});
					return messages;
				}
			}
			return null;
		}

		public bool SendMessage(string sessionKey, string msg)
		{

			int recipientId = 1;
			using (var dbContext = new DataModel())
			{

				var sessionService = new SessionService(dbContext);
				var user = sessionService.GetUser(sessionKey);
				if (user == null)
				{
					return false;
				}

				var recipient =
					dbContext.Users.Single(u => u.Id == recipientId);

				var newMsg = new Message
				{
					Content = msg,
					Sender = user,
					Recipient = recipient
				};
				dbContext.Add(newMsg);
				dbContext.SaveChanges();

				return true;
			}
		}
    }
}
