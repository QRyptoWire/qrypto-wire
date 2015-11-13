using System;
using Windows.Networking.PushNotifications;
using Cirrious.MvvmCross.Plugins.Messenger;
using Microsoft.Phone.Notification;
using QRyptoWire.Core.Messages;
using QRyptoWire.Core.Services;

namespace QRyptoWire.App.WPhone.PhoneImplementations
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

	    private const string ChannelName = "QryptoWirePushChannel";

	    private string AcquireToken()
	    {
	        var currentChannel = HttpNotificationChannel.Find(ChannelName);
	        if (currentChannel == null)
	        {
	            var channel = new HttpNotificationChannel(ChannelName);
	            channel.Open();
	            channel.BindToShellToast();

                channel.HttpNotificationReceived += OnNotificationReceived;
	            return channel.ChannelUri.ToString();
	        }
            currentChannel.Open();
            currentChannel.BindToShellToast();
	        return currentChannel.ChannelUri.ToString();
	    }

	    private void OnNotificationReceived(object sender, HttpNotificationEventArgs httpNotificationEventArgs)
	    {
            _messenger.Publish(new NotificationReceivedMessage(this));
        }

	    public void AddPushToken()
		{
            _serviceClient.RegisterPushToken(AcquireToken());
			//var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
			//channel.PushNotificationReceived += OnNotificationReceived;
			//_serviceClient.RegisterPushToken(channel.Uri);
		}
	}
}
