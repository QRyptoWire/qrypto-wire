using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.UI;
using Microsoft.SqlServer.Server;
using QRyptoWire.Service.Data;
using QRyptoWire.Service.Core;

namespace QRyptoWire.Service.Api.Controllers
{
	public class QryptoWireController : ApiController
	{

		[Route("api/Register/{deviceId}/{password}")]
		[HttpGet]
		public IHttpActionResult Register(string deviceId, string password)
		{
			using (var dbContext = new DataModel())
			{
				if (dbContext.Users
						.Any(p
							=> p.PasswordHash == password
							   && p.DeviceId == deviceId))
					return NotFound();

				var newUsr = new User
				{
					PasswordHash = password,
					AllowPush = true,
					DeviceId = deviceId
				};
				dbContext.Add(newUsr);
				dbContext.SaveChanges();
			}
			return Ok();
		}

		[Route("api/Login/{deviceId}/{password}")]
		[HttpGet]
		public IHttpActionResult Login(string deviceId, string password)
		{
			using (var dbContext = new DataModel())
			{
				var sessionService = new SessionService(dbContext);
				string sessionKey = sessionService.CreateSession(deviceId, password);
				if (sessionKey != null)
				{
					return Ok(sessionKey);
				}
				return NotFound();
			}
		}

		[Route("api/SendMessage/{sessionKey}/{msg}")]
		[HttpGet]
		public IHttpActionResult SendMessage(string sessionKey, string msg)
		{
			int recipientId = 1;
			using (var dbContext = new DataModel())
			{

				var sessionService = new SessionService(dbContext);
				var user = sessionService.GetUser(sessionKey);
				if (user == null)
				{
					return NotFound();
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
			}
			return Ok("Message " + msg + " added.");
		}

		[Route("api/FetchMessages/{sessionKey}")]
		[HttpGet]
		public IHttpActionResult FetchMessages(string sessionKey)
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
					return Ok(messages);
				}
			}
			return NotFound();
		}

		[Route("api/AddContact")]
		[HttpGet]
		public IHttpActionResult AddContact()
		{
			//TODO: do it
			return Ok();
		}

		[Route("api/FetchContacts")]
		[HttpGet]
		public IHttpActionResult FetchContacts()
		{
			//TODO: do it
			return Ok();
		}

		[Route("api/GetUserId")]
		[HttpGet]
		public IHttpActionResult GetUserId()
		{
			//TODO: do it
			return Ok();
		}


	}
}
