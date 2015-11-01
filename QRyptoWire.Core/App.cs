using System.Reflection;
using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Services;
using QRyptoWire.Core.Services.Stubs;
using QRyptoWire.Core.ViewModels;

namespace QRyptoWire.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
			
	        var useStubs = true;

	        if (useStubs)
	        {
				Mvx.RegisterType<IStorageService, StorageServiceStub>();
				Mvx.RegisterType<IUserService, UserServiceStub>();
			}
			Mvx.LazyConstructAndRegisterSingleton<IQryptoWireServiceClient, QryptoWireServiceClient>();
            RegisterAppStart<LoginViewModel>();
        }
    }
}