using System.Web.Http;
using QRyptoWire.Service.Core;

namespace QRyptoWire.Service.Api.Controllers
{
	public class QryptoWireController : ApiController
	{
		public class PasswordRequest
		{
			public string DeviceId { get; set; }
			public string Password { get; set; }
		}


		[Route("api/Register")]
		[HttpPost]
		public IHttpActionResult Register(PasswordRequest req)
		{
			var userService = new UserService();
			if (userService.Register(req.DeviceId, req.Password))
			{
				return Ok();
			}
			return NotFound();
		}

		[Route("api/Login")]
		[HttpPost]
		public IHttpActionResult Login(PasswordRequest req)
		{
			var userService = new UserService();
			var sessionKey = userService.Login(req.DeviceId, req.Password);
			
			if (sessionKey != null)
			{
				return Ok(sessionKey);
			}
			return NotFound();
		}

		[Route("api/SendMessage/{sessionKey}")]
		[HttpGet, HttpPost]
		public IHttpActionResult SendMessage([FromUri]string sessionKey, Shared.Dto.Message msg)
		{
			var messageService = new MessageService();
			if (messageService.SendMessage(sessionKey, msg))
			{
				return Ok("Message " + msg.Body + " added.");
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

		[Route("api/AddContact/{sessionKey}")]
		[HttpGet, HttpPost]
		public IHttpActionResult AddContact([FromUri]string sessionKey,Shared.Dto.Contact contact)
		{
			var contactService = new ContactService();
			if (contactService.SendContact(sessionKey, contact))
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
			return contacts != null ? 
				(IHttpActionResult) Ok(contacts) 
				: NotFound();
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

		[Route("api/RegisterPushTalken/{sessionKey}")]
		[HttpGet, HttpPost]
		public IHttpActionResult RegisterPush(string sessionKey, string pushToken)
		{
			var userService = new UserService();
			var ok = userService.RegisterPushToken(sessionKey, pushToken);

			if (ok) return Ok();
			return NotFound();
		}

		[Route("api/AllowPushes/{sessionKey}")]
		[HttpGet, HttpPost]
		public IHttpActionResult UnRegisterPush(string sessionKey, bool unregister)
		{
			var userService = new UserService();
			//var ok = userService.UnRegisterPushToken(sessionKey, unregister);

			//if (ok) return Ok();
			return NotFound();
		}

		[Route("api/test")]
		[HttpGet, HttpPost]
		public IHttpActionResult Test([FromBody]string deviceId)
		{
			 return Ok("Supcio!" + deviceId + " : " );
		}


		protected override void Dispose(bool disposing)
		{
			//DbContextFactory.Dispose();
			base.Dispose(disposing);
		}
	}
}
