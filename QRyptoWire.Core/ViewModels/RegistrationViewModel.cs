using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Services;

namespace QRyptoWire.Core.ViewModels
{
	public class RegistrationViewModel : MvxViewModel
	{
		private readonly IStorageService _storageService;
		private string _myName;

		public RegistrationViewModel(IStorageService storageService)
		{
			_storageService = storageService;

			GoCommand = new MvxCommand(GoCommandAction, () => !string.IsNullOrWhiteSpace(MyName));
		}

		public string MyName
		{
			get { return _myName; }
			set
			{
				_myName = value;
				RaisePropertyChanged();
				GoCommand.RaiseCanExecuteChanged();
			}
		}

		public IMvxCommand GoCommand { get; private set; }
		private void GoCommandAction()
		{
			_storageService.SetUserName(MyName);
			ShowViewModel<LoginViewModel>(MyName);
		}
	}
}
