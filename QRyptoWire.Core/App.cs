using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.ViewModels;
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
				
            RegisterAppStart<FirstViewModel>();
        }
    }
}