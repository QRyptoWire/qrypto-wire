using Windows.UI.Xaml.Controls;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsCommon.Platform;
using QRyptoWire.App.PhoneImplementations;
using QRyptoWire.Core.Services;

namespace QRyptoWire.App
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

	    public override void Initialize()
	    {
		    base.Initialize();
			Mvx.RegisterType<IPushService, PushService>();
		}

	    protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}