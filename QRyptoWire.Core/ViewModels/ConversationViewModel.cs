using System;
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
		private string _messageBody;

		public ConversationViewModel(IStorageService storageService, IMessageService messageService)
		{
			_storageService = storageService;
			_messageService = messageService;

			SendMessageCommand = new MvxCommand(SendMessageCommandAction, SendCommandCanExecute);
		}

		private bool SendCommandCanExecute()
		{
			return !string.IsNullOrEmpty(MessageBody);
		}

		public string ContactName { get; set; }

		public string MessageBody
		{
			get { return _messageBody; }
			set
			{
				_messageBody = value;
				RaisePropertyChanged();
			}
		}

		public void Init(int id)
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
