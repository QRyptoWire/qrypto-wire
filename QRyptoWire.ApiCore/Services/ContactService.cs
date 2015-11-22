using System.Linq;
using QRyptoWire.Service.Data;

namespace QRyptoWire.Service.Core
{
	public class ContactService : IContactService
	{
		public IQueryable<Shared.Dto.Message> FetchContacts(string sessionKey)
		{
			var dbContext = DbContextFactory.GetContext();
			//TODO: delete
			var sessionService = new SessionService();
			var user = sessionService.GetUser(sessionKey);
			if (user == null) return null;

			var messages = dbContext.Contacts
				.Where(m => m.Recipient.Id == user.Id)
				.Select(e => new Shared.Dto.Message
				{
					Body = e.Content,
					ReceiverId = e.Recipient.Id,
					SenderId = e.Sender.Id
				});
			return messages;
		}

		public bool SendContact(string sessionKey,int recipientId, string msg)
		{
			var dbContext = DbContextFactory.GetContext();

			var sessionService = new SessionService();
			var user = sessionService.GetUser(sessionKey);
			if (user == null)
			{
				return false;
			}

			var recipient =
				dbContext.Users.Single(u => u.Id == recipientId);

			var newContact = new Contact
			{
				Content = msg,
				Sender = user,
				Recipient = recipient
			};
			dbContext.Add(newContact);
			dbContext.SaveChanges();

			return true;

		}
	}
}
