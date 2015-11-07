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
			Contacts = new List<Contact>();
			Contacts.Add(new Contact {Name = "top kek"});
			SelectContactCommand = new MvxCommand<Contact>(SelectContactCommandAction);
		}

		public IList<Contact> Contacts { get; private set; } 
		public ICommand SelectContactCommand { get; private set; }

		private void SelectContactCommandAction(Contact param)
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
