using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Cirrious.MvvmCross.Plugins.Messenger;
using Microsoft.Phone.Info;
using Microsoft.Phone.Notification;
using QRyptoWire.Core.Messages;
using QRyptoWire.Core.Services;

namespace QRyptoWire.App.WPhone.PhoneImplementations
{
	public class PhoneService : IPhoneService
	{
		private readonly IQryptoWireServiceClient _serviceClient;
		private readonly IMvxMessenger _messenger;
		private readonly IMessageService _messageService;

		public PhoneService(IQryptoWireServiceClient serviceClient, IMvxMessenger messenger, IMessageService messageService)
		{
			_serviceClient = serviceClient;
			_messenger = messenger;
			_messageService = messageService;
		}

	    private const string ChannelName = "QryptoWirePushChannel";

	    private async void AcquireToken()
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
			    {
				    await Task.Delay(200);
					if(currentChannel.ChannelUri == null)
						return;
			    }

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

	    private async void OnNotificationReceived(object sender, NotificationEventArgs httpNotificationEventArgs)
	    {
		    await Task.Run(() =>
		    {
			    _messageService.FetchMessages();
				_messageService.FetchContacts();
			});
            _messenger.Publish(new NotificationReceivedMessage(this));
        }

	    public void AddPushToken()
		{
            AcquireToken();
		}

		public void LoadDeviceId()
		{
			var id = Convert.ToBase64String((byte[])DeviceExtendedProperties.GetValue("DeviceUniqueId"));
            _serviceClient.SetDeviceId(id);
		}
	}
}
