using System.Windows.Input;
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

		public SettingsViewModel(IUserService userService, IStorageService storageService, IQrService qrService)
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
			MakeApiCallAsync(() => _userService.GetPushSettings(), b => AllowPushes = b);
			Menu = new MenuViewModel(MenuMode.AtSettings);
		}

		public IMvxCommand GenerateCodeCommand { get; private set; }

		private void GenerateCodeAction()
		{
			_qrService.GenerateQrCode(ContactName);
		}

		public ICommand ClearCommand { get; private set; }

		private void ClearCommandAction()
		{
			_storageService.ClearMessages();
		}
	}
}
