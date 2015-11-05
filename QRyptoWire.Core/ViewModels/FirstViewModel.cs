using QRyptoWire.Core.Services;

namespace QRyptoWire.Core.ViewModels
{
    public class FirstViewModel : QryptoViewModel
    {
	    private readonly IQryptoWireServiceClient _client;
	    public FirstViewModel(IQryptoWireServiceClient client)
	    {
		    _client = client;
			MakeApiCallAsync(() => _client.Login("1", "2"), b =>
			{
				if (b)
					Hello = "topkek";
			});
			
	    }

	    private string _hello = "Hello MvvmCross";
        public string Hello
		{ 
			get { return _hello; }
            set
            {
                _hello = value;
	            RaisePropertyChanged();
            }
		}
    }
}
