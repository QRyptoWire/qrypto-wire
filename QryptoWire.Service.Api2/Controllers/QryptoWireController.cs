using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI;
using QRyptoWire.ApiCore.Services;

namespace QRyptoWire.Service.Controllers
{
    public class QryptoWireController : ApiController
    {
	    private readonly IUserService _userService;
	    public QryptoWireController(IUserService userService)
	    {
		    _userService = userService;
	    }

	    [HttpGet, HttpPost]
        [Route("QryptoWire/Login/deviceId/password")]
        public string Login(string deviceId, string password)
	    {
		    return _userService.Login(deviceId, password);
	    }
    }
}
