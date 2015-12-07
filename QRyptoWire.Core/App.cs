using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using QRyptoWire.Core.Services;
using QRyptoWire.Core.ViewModels;

namespace QRyptoWire.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
            Mvx.LazyConstructAndRegisterSingleton<IQryptoWireServiceClient, QryptoWireServiceClient>(); // is this needed?
            RegisterAppStart<LoginViewModel>();
        }
    }
}