using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Enums;

namespace QRyptoWire.Core.ViewModels
{
	public class MenuViewModel : MvxNavigatingObject
	{
		public MenuViewModel(MenuMode mode)
		{
			HomeCommand = new MvxCommand(HomeCommandAction, () => mode != MenuMode.AtHome);
			SettingsCommand = new MvxCommand(SettingsCommandAction, () => mode != MenuMode.AtSettings);
			AddContactCommand = new MvxCommand(AddContactCommandAction, () => mode != MenuMode.AtAddContact);
		}
		public ICommand HomeCommand { get; private set; }

		private void HomeCommandAction()
		{
			ShowViewModel<HomeViewModel>();
		}

		public ICommand SettingsCommand { get; private set; }

		private void SettingsCommandAction()
		{
			ShowViewModel<SettingsViewModel>();
		}

		public ICommand AddContactCommand { get; private set; }

		private void AddContactCommandAction()
		{
			ShowViewModel<AddContactViewModel>();
		}
	}
}
