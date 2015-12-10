using System.Threading.Tasks;
using QRyptoWire.Core.Objects;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
    public interface IQrService
    {
        /// <summary>
        /// Generate QR code and save it to saved pictures folder
        /// </summary>
        /// <param name="contactName">Name of the contact to save into QR code</param>
        /// <returns>Task</returns>
        Task GenerateQrCode(string contactName);

	    /// <summary>
	    /// Parse QR code data into QrContact object.
	    /// </summary>
	    /// <param name="qrData">QR code content</param>
	    /// <param name="contact">Contact object to load parsed data into</param>
	    /// <returns>Indication whether the parse succeded</returns>
	    bool ParseQrCode(string qrData, out QrContact contact);
    }
}