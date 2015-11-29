using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Messages;
using QRyptoWire.Core.Services;
using QRyptoWire.Shared;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.ViewModels
{
	public class ConversationViewModel : QryptoViewModel
	{
		private readonly IStorageService _storageService;
		private readonly IMessageService _messageService;
		private int _contactId;
		private string _messageBody;
		private MvxSubscriptionToken _token;

		public ConversationViewModel(IStorageService storageService, IMessageService messageService, IMvxMessenger messenger, IPopupHelper helper) : base(messenger, helper)
		{
			_storageService = storageService;
			_messageService = messageService;

			_token = _messenger.Subscribe<NotificationReceivedMessage>(OnNotificationReceived, MvxReference.Strong);
			SendMessageCommand = new MvxCommand(SendMessageCommandAction, () => !string.IsNullOrWhiteSpace(MessageBody));

			CleaningUp += () =>
			{
				_token.Dispose();
				_token = null;
			};
		}

		private async void OnNotificationReceived(object sender)
		{
			var newMessages = await Task.Run(() =>
			{
				return _messageService.FetchMessages().Where(e => e.SenderId == _contactId)
				.Select(e => new StoredMessage
				{
					Body = e.Body,
					Date = e.DateSent,
					Sent = e.ReceiverId == _contactId
				}).ToList();
			});

			foreach (var message in newMessages)
				Messages.Add(message);
		}

		public string ContactName { get; set; }

		public string MessageBody
		{
			get { return _messageBody; }
			set
			{
				_messageBody = value;
				RaisePropertyChanged();
				SendMessageCommand.RaiseCanExecuteChanged();
			}
		}

		public void Init(int id)
		{
			_contactId = id;
			Messages = new ObservableCollection<StoredMessage>(_storageService.GetMessages(_contactId).Select(e => new StoredMessage
			{
				Body = e.Body,
				Date = e.Date,
				Sent = e.SenderId != _contactId
			}));
			ContactName = _storageService.GetContactsWithNewMessageCount().First(e => e.Item1.Id == _contactId).Item1.Name;
			RaisePropertyChanged(() => ContactName);
			RaisePropertyChanged(() => Messages);
		}

		public IList<StoredMessage> Messages { get; set; } 

		public IMvxCommand SendMessageCommand { get; private set; }

		private void SendMessageCommandAction()
		{
			var message = new Message
			{
				Body = MessageBody,
				ReceiverId = _contactId,
				DateSent = QryptoTime.GetTime
			};
			MakeApiCallAsync(() => _messageService.SendMessage(message), () =>
			{
				MessageBody = string.Empty;
				Messages.Add(new StoredMessage
				{
					Body = message.Body,
					Date =  message.DateSent,
					Sent = true
				});
			});
		}

	}
}
