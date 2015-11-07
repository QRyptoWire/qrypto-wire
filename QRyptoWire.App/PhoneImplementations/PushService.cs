using System;
using QRyptoWire.Core.Services;
using Windows.Networking.PushNotifications;
using Cirrious.MvvmCross.Plugins.Messenger;
using QRyptoWire.Core.Messages;

namespace QRyptoWire.App.PhoneImplementations
{
	public class PushService : IPushService
	{
		private readonly IQryptoWireServiceClient _serviceClient;
		private readonly IMvxMessenger _messenger;

		public PushService(IQryptoWireServiceClient serviceClient, IMvxMessenger messenger)
		{
			_serviceClient = serviceClient;
			_messenger = messenger;
		}

		public async void AddPushToken()
		{
			var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
			channel.PushNotificationReceived += OnNotificationReceived;
			_serviceClient.RegisterPushToken(channel.Uri);
		}

		private void OnNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
		{
			_messenger.Publish(new NotificationReceivedMessage(this));
		}
	}
}
