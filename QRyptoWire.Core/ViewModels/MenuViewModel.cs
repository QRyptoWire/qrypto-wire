using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Enums;
using QRyptoWire.Core.Messages;

namespace QRyptoWire.Core.ViewModels
{
	public class MenuViewModel : MvxNavigatingObject
	{
		private MvxSubscriptionToken _token;
		public MenuViewModel(MenuMode mode)
		{
			if(mode != MenuMode.AtHome)
			{
				var messenger = Mvx.Resolve<IMvxMessenger>();
				_token = messenger.Subscribe<NotificationReceivedMessage>(message =>
				{
					HasReceivedNotifications = true;
					RaisePropertyChanged(() => HasReceivedNotifications);
				});
			}
			HomeCommand = new MvxCommand(HomeCommandAction, () => mode != MenuMode.AtHome);
			SettingsCommand = new MvxCommand(SettingsCommandAction, () => mode != MenuMode.AtSettings);
			AddContactCommand = new MvxCommand(AddContactCommandAction, () => mode != MenuMode.AtAddContact);
		}

		public bool HasReceivedNotifications { get; set; }

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
