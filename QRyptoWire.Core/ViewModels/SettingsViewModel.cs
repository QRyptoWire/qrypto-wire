using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Enums;
using QRyptoWire.Core.Services;

namespace QRyptoWire.Core.ViewModels
{
	public class SettingsViewModel : QryptoViewModel
	{
		private readonly IUserService _userService;
		private readonly IStorageService _storageService;
		private readonly IQrService _qrService;
		private bool _allowPushes;
		private string _contactName;

		public SettingsViewModel(IUserService userService, IStorageService storageService, IQrService qrService,
			 IMvxMessenger messenger, IPopupHelper helper) : base(messenger, helper)
		{
			_userService = userService;
			_storageService = storageService;
			_qrService = qrService;

			GenerateCodeCommand = new MvxCommand(GenerateCodeAction, () => !string.IsNullOrWhiteSpace(ContactName));
			ClearCommand = new MvxCommand(ClearCommandAction);
		}

		public string ContactName
		{
			get { return _contactName; }
			set
			{
				_contactName = value;
				RaisePropertyChanged();
				GenerateCodeCommand.RaiseCanExecuteChanged();
			}
		}

		public bool AllowPushes
		{
			get { return _allowPushes; }
			set
			{
				_allowPushes = value;
				RaisePropertyChanged();
				MakeApiCallAsync(() => _userService.SetPushSettings(AllowPushes));
			}
		}

		public MenuViewModel Menu { get; private set; }
		public override void Start()
		{			
			_allowPushes = _storageService.IsPushEnabled();
			RaisePropertyChanged(() => AllowPushes);
			Menu = new MenuViewModel(MenuMode.AtSettings);
		}

		public IMvxCommand GenerateCodeCommand { get; private set; }

		private async void GenerateCodeAction()
		{
			Working = true;
			await _qrService.GenerateQrCode(ContactName);
			Working = false;
			Dispatcher.RequestMainThreadAction(() =>
				_helper.ShowSuccessPopup("Your code has been saved to your phone's Pictures directory"));
		}

		public ICommand ClearCommand { get; private set; }

		private void ClearCommandAction()
		{
			_storageService.ClearMessages();
		}
	}
}
