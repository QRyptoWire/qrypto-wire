using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Enums;
using QRyptoWire.Core.Services;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.ViewModels
{
    public class AddContactViewModel : MvxViewModel
    {
        private readonly IQrService _qrService;
        private string _contactName;

        public AddContactViewModel(IQrService qrService)
        {
            _qrService = qrService;
            CodeDetectedCommand = new MvxCommand<string>(CodeDetectedCommandAction);
            AddContactCommand = new MvxCommand(AddContactCommandAction, AddContactCanExecute);
        }

        public string ContactName
        {
            get { return _contactName; }
            set
            {
                Contact contact;
                if (_qrService.ParseQrCode(value, out contact))
                {
                    _contactName = contact.Name;
                    RaisePropertyChanged();
                }
            }
        }

        public bool CodeDetected { get; set; }

        public ICommand CodeDetectedCommand { get; private set; }

        private void CodeDetectedCommandAction(string content)
        {
            CodeDetected = true;
            RaisePropertyChanged(() => CodeDetected);
            ContactName = content;
        }

        public ICommand AddContactCommand { get; private set; }

        private void AddContactCommandAction()
        {
            ShowViewModel<HomeViewModel>();
        }

        private bool AddContactCanExecute()
        {
            return !string.IsNullOrWhiteSpace(ContactName);
        }

        public MenuViewModel Menu { get; private set; }

        public override void Start()
        {
            Menu = new MenuViewModel(MenuMode.AtAddContact);
        }
    }
}
