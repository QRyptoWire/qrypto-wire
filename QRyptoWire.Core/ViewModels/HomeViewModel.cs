using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.DbItems;
using QRyptoWire.Core.Enums;
using QRyptoWire.Core.Services;

namespace QRyptoWire.Core.ViewModels
{
	public class HomeViewModel : MvxViewModel
	{
		private readonly IStorageService _storageService;

		public HomeViewModel(IStorageService storageService)
		{
			_storageService = storageService;
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
			Contacts = _storageService.GetContactsWithNewMessageCount().Select(e => new ContactListItem
			{
				Id = e.Item1.Id,
				Name = e.Item1.Name,
				NewContact = e.Item1.IsNew,
				UnreadMessages = e.Item2
			}).ToList();
			_storageService.MarkContactsAsNotNew(Contacts.Select(e => e.Id).ToList());
			Menu = new MenuViewModel(MenuMode.AtHome);
		}
	}
}
