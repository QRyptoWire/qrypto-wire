using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Enums;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.ViewModels
{
	public class HomeViewModel : MvxViewModel
	{
		public HomeViewModel()
		{
			Contacts = new List<ContactListItem>();
			Contacts.Add(new ContactListItem { Name = "top kek", UnreadMessages = 1});
			SelectContactCommand = new MvxCommand<ContactListItem>(SelectContactCommandAction);
		}

		public List<ContactListItem> Contacts { get; private set; } 
		public ICommand SelectContactCommand { get; private set; }

		private void SelectContactCommandAction(ContactListItem param)
		{
			ShowViewModel<LoginViewModel>();
		}
		public MenuViewModel Menu { get; private set; }

		public override void Start()
		{
			Menu = new MenuViewModel(MenuMode.AtHome);
		}
	}
}
