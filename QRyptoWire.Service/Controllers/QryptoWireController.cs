using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QRyptoWire.Service.Api3.Controllers
{
    public class QryptoWireController : ApiController
    {

		public IHttpActionResult GetDoStuff()
		{
			return Ok("I did it! I did it mon!!!");
		}
	}
}
