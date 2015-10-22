using System.Diagnostics;
using Cirrious.MvvmCross.ViewModels;
using QRyptoWire.Core.Services;

namespace QRyptoWire.Core.ViewModels
{
    public class FirstViewModel 
		: MvxViewModel
    {
	    private readonly IQryptoWireServiceClient _client;
	    public FirstViewModel(IQryptoWireServiceClient client)
	    {
		    _client = client;
			var test = _client.Login("1", "1");
			if(test)
				Debugger.Break();
	    }

	    private string _hello = "Hello MvvmCross";
        public string Hello
		{ 
			get { return _hello; }
            set
            {
                _hello = value; RaisePropertyChanged();
            }
		}
    }
}
