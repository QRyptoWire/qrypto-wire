using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Enums;
using QRyptoWire.Core.Services;

namespace QRyptoWire.Core.ViewModels
{
	public class SettingsViewModel : QryptoViewModel
	{
		private readonly IUserService _userService;

		public SettingsViewModel(IUserService userService)
		{
			_userService = userService;

			ConfirmationCommand = new MvxCommand(ConfirmationCommandAction);
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

		public ICommand ConfirmationCommand { get; private set; }

		private void ConfirmationCommandAction()
		{
			MakeApiCallAsync(() => _userService.SetPushSettings(AllowPushes));
		}
	}
}
