using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.DbItems;
using QRyptoWire.Core.Enums;
using QRyptoWire.Core.Services;

namespace QRyptoWire.Core.ViewModels
{
	public class HomeViewModel : QryptoViewModel
	{
		private readonly IStorageService _storageService;
		private readonly IMessageService _messageService;

		public HomeViewModel(IStorageService storageService, IMvxMessenger messenger, IPopupHelper helper, IMessageService messageService) : base(messenger, helper)
		{
			_storageService = storageService;
			_messageService = messageService;
			SelectContactCommand = new MvxCommand<ContactListItem>(SelectContactCommandAction);
		}

		public List<ContactListItem> Contacts { get; private set; } 
		public ICommand SelectContactCommand { get; private set; }

		private void SelectContactCommandAction(ContactListItem param)
		{
			ShowViewModel<ConversationViewModel>(new {id = param.Id});
		}
		public MenuViewModel Menu { get; private set; }

		public override void Start()
		{
			MakeApiCallAsync(() =>
			{
				_messageService.FetchMessages();
				_messageService.FetchContacts();
				return true;
			}, b =>
			{
				Contacts = _storageService.GetContactsWithNewMessageCount().Select(e => new ContactListItem
				{
					Id = e.Item1.Id,
					Name = e.Item1.Name,
					NewContact = e.Item1.IsNew,
					UnreadMessages = e.Item2
				}).ToList();
				_storageService.MarkContactsAsNotNew(Contacts.Select(e => e.Id).ToList());
			});
			Menu = new MenuViewModel(MenuMode.AtHome);
		}
	}
}
