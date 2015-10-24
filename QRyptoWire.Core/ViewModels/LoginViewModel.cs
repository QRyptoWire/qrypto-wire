using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Services;

namespace QRyptoWire.Core.ViewModels
{
	public class LoginViewModel : QryptoViewModel
	{
		private readonly IStorageService _storageService;
		private readonly IUserService _userService;
		private bool _registering;
		private string _password;

		public LoginViewModel(IStorageService storageService, IUserService userService)
		{
			_storageService = storageService;
			_userService = userService;

			if (!_storageService.PublicKeyExists())
				Registering = true;

			ProceedCommand = new MvxCommand(ProceedCommandAction, ValidatePassword);
		}

		public bool Registering
		{
			get { return _registering; }
			set
			{
				_registering = value; 
				RaisePropertyChanged();
			}
		}

		public string Password
		{
			get { return _password; }
			set
			{
				_password = value; 
				ProceedCommand.RaiseCanExecuteChanged();
			}
		}

		private bool ValidatePassword()
		{
			if (string.IsNullOrWhiteSpace(Password) || Password.Length < 8)
				return false;
			return true;
		}

		public IMvxCommand ProceedCommand { get; private set; }
		private void ProceedCommandAction()
		{
			Password += Password;
		}
	}
}
