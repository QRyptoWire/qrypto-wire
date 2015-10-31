using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Enums;

namespace QRyptoWire.Core.ViewModels
{
	public class HomeViewModel : MvxViewModel
	{
		public HomeViewModel()
		{
			Contacts = new List<int>();
			Contacts.Add(1);
			SelectContactCommand = new MvxCommand<int>(SelectContactCommandAction);
		}

		public IList<int> Contacts { get; private set; } 
		public ICommand SelectContactCommand { get; private set; }

		private void SelectContactCommandAction(int val)
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
