using System.Web.Http;
using QRyptoWire.Service.Core;

namespace QRyptoWire.Service.Api.Controllers
{
	public class QryptoWireController : ApiController
	{

		[Route("api/Register/{deviceId}/{password}")]
		[HttpGet]
		public IHttpActionResult Register(string deviceId, string password)
		{
			var userService = new UserService();
			if (userService.Register(deviceId, password))
			{
				return Ok();
			}
			return NotFound();
		}

		[Route("api/Login/{deviceId}/{password}")]
		[HttpGet]
		public IHttpActionResult Login(string deviceId, string password)
		{
			var userService = new UserService();
			var sessionKey = userService.Login(deviceId, password);
			
			if (sessionKey != null)
			{
				return Ok(sessionKey);
			}
			return NotFound();
		}

		[Route("api/SendMessage/{sessionKey}/{recipientId}/{msg}")]
		[HttpGet]
		public IHttpActionResult SendMessage(string sessionKey, int recipientId, string msg)
		{
			var messageService = new MessageService();
			if (messageService.SendMessage(sessionKey, recipientId, msg))
			{
				return Ok("Message " + msg + " added.");
			}
			return NotFound();
		}

		[Route("api/FetchMessages/{sessionKey}")]
		[HttpGet]
		public IHttpActionResult FetchMessages(string sessionKey)
		{

			var messageService = new MessageService();
			var messages = messageService.FetchMessages(sessionKey);
            if ( messages!=null)
			{
				return Ok( messages );
			}
			return NotFound();
		}

		[Route("api/AddContact/{sessionKey}/{recipientId}/{contact}")]
		[HttpGet]
		public IHttpActionResult AddContact(string sessionKey, int recipientId, string contact)
		{
			var contactService = new ContactService();
			if (contactService.SendContact(sessionKey, recipientId, contact))
			{
				return Ok();
			}
			return NotFound();
		}

		[Route("api/FetchContacts/{sessionKey}")]
		[HttpGet]
		public IHttpActionResult FetchContacts(string sessionKey)
		{
			var contactService = new ContactService();
			var contacts = contactService.FetchContacts(sessionKey);
			if (contacts != null)
			{
				return Ok(contacts);
			}
			return NotFound();
		}

		[Route("api/GetUserId/{sessionKey}")]
		[HttpGet]
		public IHttpActionResult GetUserId(string sessionKey)
		{
			var sessionService = new SessionService();
			var user = sessionService.GetUser(sessionKey);
			if (user!=null) return Ok(user.Id);
			return NotFound();
		}

		[Route("api/Push/{sessionKey}/{message}")]
		[HttpGet]
		public IHttpActionResult Push(string sessionKey, string message)
		{
			var userService = new UserService();
			var sessionService = new SessionService();
			var ok = userService.Push(
				sessionService.GetUser(sessionKey).Id, 
				message
				);

			if (ok) return Ok();
			return NotFound();
		}

		[Route("api/RegisterPushTalken/{sessionKey}/{pushToken}")]
		[HttpGet]
		public IHttpActionResult RegisterPush(string sessionKey, string pushToken)
		{
			var userService = new UserService();
			var ok = userService.RegisterPushToken(sessionKey, pushToken);
			
			if (ok) return Ok();
			return NotFound();
		}


		protected override void Dispose(bool disposing)
		{
			DbContextFactory.Dispose();
			base.Dispose(disposing);
		}
	}
}
