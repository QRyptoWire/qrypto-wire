using System.Web.Http;
using QRyptoWire.ApiCore.Services;
using QRyptoWire.Service.Core;

namespace QRyptoWire.Service.Api.Controllers
{
	public class QryptoWireController : ApiController
	{
		public class SetPushAllowedRequest
		{
			public bool IsAllowed { get; set; }
		}
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
			return Ok(
			userService.Register(req.DeviceId, req.Password)
			);
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
			return Unauthorized();
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
			return Unauthorized();
		}

		[Route("api/FetchMessages/{sessionKey}")]
		[HttpGet]
		public IHttpActionResult FetchMessages(string sessionKey)
		{

			var messageService = new MessageService();
			var messages = messageService.FetchMessages(sessionKey);
			if (messages != null)
			{
				return Ok(messages);
			}
			return Unauthorized();
		}

		[Route("api/AddContact/{sessionKey}")]
		[HttpGet, HttpPost]
		public IHttpActionResult AddContact([FromUri]string sessionKey, Shared.Dto.Contact contact)
		{
			var contactService = new ContactService();
			if (contactService.SendContact(sessionKey, contact))
			{
				return Ok();
			}
			return Unauthorized();
		}

		[Route("api/FetchContacts/{sessionKey}")]
		[HttpGet]
		public IHttpActionResult FetchContacts(string sessionKey)
		{
			var contactService = new ContactService();
			var contacts = contactService.FetchContacts(sessionKey);
			return contacts != null ?
				(IHttpActionResult)Ok(contacts)
				: Unauthorized();
		}

		[Route("api/GetUserId/{sessionKey}")]
		[HttpGet]
		public IHttpActionResult GetUserId(string sessionKey)
		{
			var sessionService = new SessionService();
			var user = sessionService.GetUser(sessionKey);
			if (user != null) return Ok(user.Id);
			return Unauthorized();
		}

		[Route("api/RegisterPushToken/{sessionKey}")]
		[HttpPost]
		public IHttpActionResult RegisterPushToken([FromUri]string sessionKey, [FromBody]string pushToken)
		{
			var pushService = new PushService();
			var ok = pushService.RegisterPushToken(sessionKey, pushToken);

			if (ok) return Ok();
			return Unauthorized();
		}

		[Route("api/SetPushAllowed/{sessionKey}")]
		[HttpPost]
		public IHttpActionResult IsPushAllowed([FromUri]string sessionKey, SetPushAllowedRequest request)
		{
			var pushService = new PushService();
			var ok = pushService.SetPushAllowed(sessionKey, request.IsAllowed);

			if (ok) return Ok();
			return Unauthorized();
		}

		[Route("api/IsPushAllowed/{sessionKey}")]
		[HttpGet]
		public IHttpActionResult SetPushAllowed([FromUri]string sessionKey)
		{
			if (!new SessionService().ValidateSession(sessionKey)) return Unauthorized();
			var pushService = new PushService();
			var isAllowed = pushService.IsPushAllowed(sessionKey);
			return Ok(isAllowed);
		}


		[Route("api/test")]
		[HttpGet]
		public IHttpActionResult Test()
		{
			return Ok("Working like a boss!");
		}


		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
