using System;
using System.Collections.Generic;
using System.Linq;
using QRyptoWire.Service.Data;
using Telerik.OpenAccess;

namespace QRyptoWire.Service.Core
{
	public class ContactService : IContactService
	{
		public List<Shared.Dto.Contact> FetchContacts(string sessionKey)
		{
			var dbContext = DbContextFactory.GetContext();

			var sessionService = new SessionService();
			var user = sessionService.GetUser(sessionKey);
			if (user == null) return null;

			var contacts = dbContext.Contacts
				.Where(m => m.Recipient.Id == user.Id);

				var dtoContacts = contacts
				.Select(e => new Shared.Dto.Contact
				{
					Name = e.Name,
					PublicKey = e.PublicKey,
					ReceiverId = e.Recipient.Id,
					SenderId = e.Sender.Id
				}).ToList();

			contacts.DeleteAll();
			dbContext.SaveChanges();
			return dtoContacts;
		}

		public bool SendContact(string sessionKey, Shared.Dto.Contact contact)
		{
			var dbContext = DbContextFactory.GetContext();

			var sessionService = new SessionService();
			var user = sessionService.GetUser(sessionKey);
			if (user == null) return false;


			User recipient;
			try
			{
				recipient =
					dbContext.Users.Single(u => u.Id == contact.ReceiverId);
			}
			catch (Exception)
			{
				return false;
			}

			var newContact = new Contact
			{
				PublicKey = contact.PublicKey,
				Name = contact.Name,
				Sender = user,
				Recipient = recipient
			};

			dbContext.Add(newContact);
			dbContext.SaveChanges();

			return true;
		}
	}
}
