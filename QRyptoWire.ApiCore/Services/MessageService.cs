using System;
using System.Collections.Generic;
using System.Linq;
using QRyptoWire.ApiCore.Services;
using QRyptoWire.Service.Data;
using Telerik.OpenAccess;

namespace QRyptoWire.Service.Core
{
	public class MessageService : IMessageService
	{
		public List<Shared.Dto.Message> FetchMessages(string sessionKey)
		{
			var dbContext = DbContextFactory.GetContext();

			var sessionService = new SessionService();
			var user = sessionService.GetUser(sessionKey);
			if (user == null) return null;

			var messages = dbContext.Messages
				.Where(m => m.RecipientId == user.Id);

			var dtoMessages = messages.Select(e => new Shared.Dto.Message
			{
				Body = e.Content,
				ReceiverId = e.RecipientId,
				SenderId = e.SenderId,
				DateSent = e.SentTime,
				SymmetricKey = e.SessionKey,
				Iv = e.InitVector,
				DigitalSignature = e.Signature
			}).ToList();
			messages.DeleteAll();
			dbContext.SaveChanges();
			return dtoMessages;
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

			User recipient;
			try
			{
				recipient =
					dbContext.Users.Single(u => u.Id == msg.ReceiverId);
			}
			catch (Exception)
			{
				return false;
			}

			var newMsg = new Message
			{
				Content = msg.Body,
				Sender = user,
				Recipient = recipient,
				SentTime = msg.DateSent,
				Signature = msg.DigitalSignature,
				SessionKey = msg.SymmetricKey,
				InitVector = msg.Iv

			};

			var userService = new PushService();
			userService.Push(recipient.PushToken, "You have new message");

			dbContext.Add(newMsg);
			dbContext.SaveChanges();

			return true;
		}

	}
}
