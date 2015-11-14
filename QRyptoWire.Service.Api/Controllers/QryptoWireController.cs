using System.Linq;
using System.Web.Http;
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
			var userService = new UserService();
			if (userService.Register(deviceId, password))
			{
				return NotFound();
			}
			return Ok();
		}

		[Route("api/Login/{deviceId}/{password}")]
		[HttpGet]
		public IHttpActionResult Login(string deviceId, string password)
		{
			var userService = new UserService();
			string sessionKey = userService.Login(deviceId, password);
			
			if (sessionKey != null)
			{
				return Ok(sessionKey);
			}
			return NotFound();
		}

		[Route("api/SendMessage/{sessionKey}/{msg}")]
		[HttpGet]
		public IHttpActionResult SendMessage(string sessionKey, string msg)
		{
			var messageService = new MessageService();
			if (messageService.SendMessage(sessionKey, msg))
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

		[Route("api/AddContact/{sessionKey}/{contact}")]
		[HttpGet]
		public IHttpActionResult AddContact(string sessionKey, string contact)
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
			if (contacts != null)
			{
				return Ok(contacts);
			}
			return NotFound();
		}

		[Route("api/GetUserId/{sessionKey}")]
		[HttpGet]
		public IHttpActionResult GetUserId()
		{
			
			return Ok();
		}


	}
}
