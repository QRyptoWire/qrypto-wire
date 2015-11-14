using System.Linq;
using QRyptoWire.Service.Core;
using QRyptoWire.Service.Data;

namespace QRyptoWire.Service.Core
{
	public class ContactService : IContactService
	{
		public IQueryable<Shared.Dto.Message> FetchContacts(string sessionKey)
		{
			//TODO: delete
			using (var dbContext = new DataModel())
			{
				var sessionService = new SessionService(dbContext);
				var user = sessionService.GetUser(sessionKey);
				if (user != null)
				{
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
			}
			return null;
		}

		public bool SendContact(string sessionKey, string msg)
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
}
