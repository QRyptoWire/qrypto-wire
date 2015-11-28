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
	    private readonly IMessageService _messageService;
	    private string _contactName;
	    private Contact _contact;

        public AddContactViewModel(IQrService qrService, IMessageService messageService)
        {
            _qrService = qrService;
	        _messageService = messageService;
	        CodeDetectedCommand = new MvxCommand<string>(CodeDetectedCommandAction);
            AddContactCommand = new MvxCommand(AddContactCommandAction, AddContactCanExecute);
        }

        public string ContactName
        {
            get { return _contactName; }
            set
            {
                if (_qrService.ParseQrCode(value, out _contact))
                {
                    _contactName = _contact.Name;
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
            _messageService.AddContact(_contact);
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
