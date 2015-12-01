using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.DbItems;
using QRyptoWire.Core.Enums;
using QRyptoWire.Core.Services;

namespace QRyptoWire.Core.ViewModels
{
	public class LoginViewModel : QryptoViewModel
	{
		private readonly IStorageService _storageService;
		private readonly IUserService _userService;
		private readonly IPhoneService _phoneService;
		private readonly IMessageService _messageService;
	    private readonly IEncryptionService _encryptionService;
	    private bool _registering;
		private string _password;
		private string _errorMessage;

		public LoginViewModel(IStorageService storageService, IUserService userService, IPhoneService phoneService, 
			IMessageService messageService, IEncryptionService encryptionService, IMvxMessenger messenger, IPopupHelper helper) : base(messenger, helper)
		{
			_storageService = storageService;
			_userService = userService;
			_phoneService = phoneService;
			_messageService = messageService;
		    _encryptionService = encryptionService;

		    ProceedCommand = new MvxCommand(ProceedCommandAction, ValidatePassword);
		}

		public override void Start()
		{
            if (!_storageService.UserExists())
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
		        MakeApiCallAsync(() =>
		        {
		            var userId = _userService.Register(Password);
		            if (userId != 0)
		            {
		                _storageService.SaveUser(new UserItem {Id = userId, KeyPair = _encryptionService.GetKeyPair()});
		            }
		            return userId;
		        }, ret =>
		        {
		            if (ret != 0)
		                ShowViewModel<RegistrationViewModel>();
		        });
		    else
		    {
                MakeApiCallAsync(() =>
		        {
		            var loggedIn = _userService.Login(Password);
		            if (loggedIn)
		            {
						_messageService.FetchMessages();
		                _messageService.FetchContacts();
		            }
		            return loggedIn;
		        }, b =>
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
}
