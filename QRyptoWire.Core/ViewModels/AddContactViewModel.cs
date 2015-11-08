using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Enums;

namespace QRyptoWire.Core.ViewModels
{
	public class AddContactViewModel : MvxViewModel
	{
		private string _decryptedStuff;

		public AddContactViewModel()
		{
			CodeDetectedCommand = new MvxCommand<string>(CodeDetectedCommandAction);
			AddContactCommand = new MvxCommand(AddContactCommandAction, AddContactCanExecute);
		}

		public string DecryptedStuff
		{
			get { return _decryptedStuff; }
			set
			{
				_decryptedStuff = value;
				RaisePropertyChanged();
			}
		}

		public ICommand CodeDetectedCommand { get; private set; }

		private void CodeDetectedCommandAction(string content)
		{
			DecryptedStuff = content;
		}

		public ICommand AddContactCommand { get; private set; }

		private void AddContactCommandAction()
		{
			ShowViewModel<HomeViewModel>();
		}

		private bool AddContactCanExecute()
		{
			return string.IsNullOrWhiteSpace(DecryptedStuff);
		}

		public MenuViewModel Menu { get; private set; }
		
		public override void Start()
		{
			Menu = new MenuViewModel(MenuMode.AtAddContact);
		}
	}
}
