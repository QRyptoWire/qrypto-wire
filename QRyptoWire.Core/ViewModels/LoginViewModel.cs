using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Enums;
using QRyptoWire.Core.Services;

namespace QRyptoWire.Core.ViewModels
{
	public class LoginViewModel : QryptoViewModel
	{
		private readonly IStorageService _storageService;
		private readonly IUserService _userService;
		private readonly IPhoneService _phoneService;
		private bool _registering;
		private string _password;
		private string _errorMessage;

		public LoginViewModel(IStorageService storageService, IUserService userService, IPhoneService phoneService)
		{
			_storageService = storageService;
			_userService = userService;
			_phoneService = phoneService;

			ProceedCommand = new MvxCommand(ProceedCommandAction, ValidatePassword);
		}

		public override void Start()
		{
			if (!_storageService.PublicKeyExists())
				Registering = true;
			_phoneService.LoadDeviceId();
			Menu = new MenuViewModel(MenuMode.AtHome);
		}

		public MenuViewModel Menu { get; set; }

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

		public string ErrorMessage
		{
			get { return _errorMessage; }
			set
			{
				_errorMessage = value;
				RaisePropertyChanged();
			}
		}

		private async void InitSynchronizationTasks()
		{
			await Task.Run(() => _phoneService.AddPushToken());
		}

		public IMvxCommand ProceedCommand { get; private set; }
		private void ProceedCommandAction()
		{
			if (Registering)
				MakeApiCallAsync(() => _userService.Register(Password), b =>
				{
					if (b)
						ShowViewModel<RegistrationViewModel>();
				});
			else
				MakeApiCallAsync(() => _userService.Login(Password), b =>
				{
					if (b)
					{	
						InitSynchronizationTasks();
						ShowViewModel<HomeViewModel>();
					}
					else
						ErrorMessage = "Invalid password";
				});
		}
	}
}
