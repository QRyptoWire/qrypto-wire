using System;
using System.Collections.Generic;
using System.Linq;
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
				SessionKey = e.SessionKey,
				InitVector = e.InitVector,
				Signature = e.Signature
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
				Signature = msg.Signature,
				SessionKey = msg.SessionKey,
				InitVector = msg.InitVector

			};

			var userService = new UserService();
			userService.Push(recipient.PushToken);

			dbContext.Add(newMsg);
			dbContext.SaveChanges();

			return true;
		}

	}
}
