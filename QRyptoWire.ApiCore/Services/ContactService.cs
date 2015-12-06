using System;
using System.Collections.Generic;
using System.Linq;
using QRyptoWire.ApiCore.Services;
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
					SenderId = e.SenderId,
					Name = e.Name,
					PublicKey = e.PublicKey
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

			var userService = new PushService();
			userService.Push(recipient, "Recieved new contact!");

			return true;
		}
	}
}
