﻿namespace QRyptoWire.Service.Core
{
    public interface IUserService
	{
		string Login(string deviceId, string password);
		int Register(string deviceId, string password);
	    int GetId(string sessionKey);
	}
	
}
