using System.Linq;
using QRyptoWire.Service.Data;

namespace QRyptoWire.Service.Core
{
	public class MessageService : IMessageService
	{
		public IQueryable<Shared.Dto.Message> FetchMessages(string sessionKey)
		{
			//TODO: delete

			var dbContext = DbContextFactory.GetContext();
			var sessionService = new SessionService();
			var user = sessionService.GetUser(sessionKey);
			if (user == null) return null;
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

		public bool SendMessage(string sessionKey, Shared.Dto.Message msg)
		{
			var dbContext = DbContextFactory.GetContext();

			var sessionService = new SessionService();
			var user = sessionService.GetUser(sessionKey);
			if (user == null)
			{
				return false;
			}

			var recipient =
				dbContext.Users.Single(u => u.Id == msg.ReceiverId);

			var newMsg = new Message
			{
				Content = msg.Body,
				Sender = user,
				Recipient = recipient,
				SentTime = msg.Time,
				Signature = msg.Signature
			};
			dbContext.Add(newMsg);
			dbContext.SaveChanges();

			return true;
		}

	}
}
