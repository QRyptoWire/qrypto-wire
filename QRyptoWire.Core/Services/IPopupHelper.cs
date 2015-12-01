namespace QRyptoWire.Core.Services
{
	public interface IPopupHelper
	{
		void ShowRequestFailedPopup();
		void ShowSuccessPopup(string messageBody);
	}
}
