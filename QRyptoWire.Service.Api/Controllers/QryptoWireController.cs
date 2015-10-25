using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using QRyptoWire.Service.Data;

namespace QRyptoWire.Service.Api.Controllers
{
	public class QryptoWireController : ApiController
    {

		[Route("api/Register/{deviceId}/{password}")]
		[HttpGet]
		public IHttpActionResult Register(string deviceId, string password)
		{
			//TODO: add user
			return Ok();
		}

		[Route("api/Login/{deviceId}/{password}")]
		[HttpGet]
		public IHttpActionResult Login(string deviceId, string password)
		{
			//TODO: login,  return session key
			return Ok();
		}

		[Route("api/SendMessage/{msg}")]
		[HttpGet]
		public IHttpActionResult SendMessage(string msg)
		{
			//TODO: verify session key
			using (var dbContext = new DataModel())
			{
				var newMsg = new Message { Content = msg };
				dbContext.Add(newMsg);
				dbContext.SaveChanges();
			}
			return Ok("Message " + msg + " added.");
		}

		[Route("api/FetchMessages")]
		[HttpGet]
		public IHttpActionResult FetchMessages()
		{
			//TODO: verify session key, send based on uuid, delete
			var ret = "";
			using (var dbContext = new DataModel())
			{
				IEnumerable<Message> messages = dbContext.Messages.ToList();
				ret = messages.Aggregate(ret, (current, msg) => current + ' ' + msg.Content);
			}
			return Ok(ret);
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
