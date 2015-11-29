using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace QRyptoWire.Core.ViewModels
{
	public class RegistrationViewModel : MvxViewModel
	{
		public RegistrationViewModel()
		{
			GoCommand = new MvxCommand(() => ShowViewModel<LoginViewModel>());
		}

		public ICommand GoCommand { get; private set; }
	}
}
