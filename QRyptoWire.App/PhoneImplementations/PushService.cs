using System;
using QRyptoWire.Core.Services;
using Windows.Networking.PushNotifications;

namespace QRyptoWire.App.PhoneImplementations
{
	public class PushService : IPushService
	{
		private readonly IQryptoWireServiceClient _serviceClient;

		public PushService(IQryptoWireServiceClient serviceClient)
		{
			_serviceClient = serviceClient;
		}

		public async void AddPushToken()
		{
			var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
			_serviceClient.RegisterPushToken(channel.Uri);
		}
	}
}
