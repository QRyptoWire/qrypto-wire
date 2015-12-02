namespace QRyptoWire.Core.Services
{
	public interface IPopupHelper
	{
		void ShowRequestFailedPopup(string messageBody);
		void ShowSuccessPopup(string messageBody);
	}
}
