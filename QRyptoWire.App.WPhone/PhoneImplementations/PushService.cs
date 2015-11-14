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

	    private void AcquireToken()
	    {
	        var currentChannel = HttpNotificationChannel.Find(ChannelName);
		    if (currentChannel == null)
		    {
			    currentChannel = new HttpNotificationChannel(ChannelName);
				currentChannel.Open();
				currentChannel.BindToShellToast();
				currentChannel.ChannelUriUpdated += (sender, args) =>
				{
					RegisterToken(currentChannel.ChannelUri.AbsoluteUri);
				};

				if (currentChannel.ChannelUri == null)
				    return;

			    RegisterToken(currentChannel.ChannelUri.AbsoluteUri);
		    }
		    else
		    {
				currentChannel.ChannelUriUpdated += (sender, args) =>
				{
					RegisterToken(currentChannel.ChannelUri.AbsoluteUri);
				};
			}

			currentChannel.ShellToastNotificationReceived += OnNotificationReceived;
		}

		private static string _channelUri;
		private static bool _registered = false;
		private static readonly object _lockObject = new object();

		private void RegisterToken(string token)
		{
			lock (_lockObject)
			{
				if(_channelUri != null && _channelUri ==token && _registered)
					return;

				_serviceClient.RegisterPushToken(token);
				_channelUri = token;
				_registered = true;
			}
		}

	    private void OnNotificationReceived(object sender, NotificationEventArgs httpNotificationEventArgs)
	    {
            _messenger.Publish(new NotificationReceivedMessage(this));
        }

	    public void AddPushToken()
		{
            AcquireToken();
		}
	}
}
