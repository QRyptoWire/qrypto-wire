using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Enums;
using QRyptoWire.Core.Objects;
using QRyptoWire.Core.Services;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.ViewModels
{
    public class AddContactViewModel : QryptoViewModel
    {
        private readonly IQrService _qrService;
	    private readonly IMessageService _messageService;
	    private string _contactName;
	    private QrContact _contact;

        public AddContactViewModel(IQrService qrService, IMessageService messageService, IMvxMessenger messenger, IPopupHelper helper) : base(messenger, helper)
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
		        _contactName = value;
		        RaisePropertyChanged();
				AddContactCommand.RaiseCanExecuteChanged();
	        }
        }

        public bool CodeDetected { get; set; }

        public ICommand CodeDetectedCommand { get; private set; }

        private void CodeDetectedCommandAction(string content)
        {
            CodeDetected = true;
            RaisePropertyChanged(() => CodeDetected);
			if (_qrService.ParseQrCode(content, out _contact))
				ContactName = _contact.Name;
		}

        public IMvxCommand AddContactCommand { get; private set; }

        private void AddContactCommandAction()
        {
            MakeApiCallAsync(() =>_messageService.AddContact(_contact));
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
