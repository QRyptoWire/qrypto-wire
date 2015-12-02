using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using Microsoft.Phone.Controls;
using QRyptoWire.App.WPhone.PhoneImplementations;
using QRyptoWire.App.WPhone.Utilities;
using QRyptoWire.Core.Services;

namespace QRyptoWire.App.WPhone
{
    public class Setup : MvxPhoneSetup
    {
        public Setup(PhoneApplicationFrame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        public override void Initialize()
        {
            base.Initialize();
            Mvx.RegisterType<IPhoneService, PhoneService>();
            Mvx.RegisterType<IQrService, QrService>();
            Mvx.RegisterType<IStorageService, StorageService>();
			Mvx.RegisterType<IPopupHelper, PopupHelper>();
            Mvx.RegisterType<IEncryptionService, EncryptionService>();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}