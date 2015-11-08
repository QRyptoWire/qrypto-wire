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

		public SettingsViewModel(IUserService userService, IStorageService storageService)
		{
			_userService = userService;
			_storageService = storageService;

			ConfirmationCommand = new MvxCommand(ConfirmationCommandAction);
			ClearCommand = new MvxCommand(ClearCommandAction);
		}

		public bool AllowPushes { get; set; }
		public MenuViewModel Menu { get; private set; }
		public override void Start()
		{
			MakeApiCallAsync(() => _userService.GetPushSettings(), b =>
			{
				AllowPushes = b;
				RaisePropertyChanged(() => AllowPushes);
			});
			Menu =new MenuViewModel(MenuMode.AtSettings);
		}

		public ICommand ClearCommand { get; private set; }

		private void ConfirmationCommandAction()
		{
			MakeApiCallAsync(() => _userService.SetPushSettings(AllowPushes));
		}

		public ICommand ConfirmationCommand { get; private set; }

		private void ClearCommandAction()
		{
			_storageService.ClearMessages();
		}
	}
}
