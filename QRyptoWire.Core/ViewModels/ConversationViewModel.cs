using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Services;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.ViewModels
{
	public class ConversationViewModel : QryptoViewModel
	{
		private readonly IStorageService _storageService;
		private readonly IMessageService _messageService;
		private int _contactId;

		public ConversationViewModel(IStorageService storageService, IMessageService messageService)
		{
			_storageService = storageService;
			_messageService = messageService;

			SendMessageCommand = new MvxCommand(SendMessageCommandAction, () => !string.IsNullOrEmpty(MessageBody));
		}

		public string ContactName { get; set; }
		public string MessageBody { get; set; }

		public void Initialize(int id)
		{
			_contactId = id;
			Messages = new ObservableCollection<string>(_storageService.GetMessages(_contactId));
			ContactName = _storageService.GetContacts().First(e => e.Id == _contactId).Name;
			RaisePropertyChanged(() => ContactName);
			RaisePropertyChanged(() => Messages);
		}

		public IList<string> Messages { get; set; } 

		public ICommand SendMessageCommand { get; private set; }

		private void SendMessageCommandAction()
		{
			_messageService.SendMessage(new Message
			{
				Body = MessageBody,
				ReceiverId = _contactId
			});
		}

	}
}
