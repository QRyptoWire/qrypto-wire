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
			string sessionKey = SessionService.CreateSession(deviceId, password);
			if(sessionKey != null)
			{
				return Ok(sessionKey);
			}
			return NotFound();
		}

		[Route("api/SendMessage/{sessionKey}/{msg}")]
		[HttpGet]
		public IHttpActionResult SendMessage( string sessionKey, string msg)
		{
			int recipientId = 1;
			var user = SessionService.GetUser(sessionKey);
			if ( user == null)
			{
				return NotFound();
			}

			using (var dbContext = new DataModel())
			{
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
			var user = SessionService.GetUser(sessionKey);
			if (user != null)
			{
				IEnumerable<Message> messages;
                using (var dbContext = new DataModel())
				{
					 messages = dbContext.Messages.Where(m => m.RecipientId == user.Id).ToList();
					//ret = messages.Aggregate(ret, (current, msg) => current + ' ' + msg.Content);
				}
				return Ok(messages);
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
