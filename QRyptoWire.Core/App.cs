using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using QRyptoWire.Core.Services;
using QRyptoWire.Core.Services.Stubs;
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
				
            Mvx.RegisterType<IStorageService, StorageServiceStub>();
           // Mvx.RegisterType<IUserService, UserServiceStub>();

            Mvx.LazyConstructAndRegisterSingleton<IQryptoWireServiceClient, QryptoWireServiceClient>();
            RegisterAppStart<LoginViewModel>();
        }
    }
}