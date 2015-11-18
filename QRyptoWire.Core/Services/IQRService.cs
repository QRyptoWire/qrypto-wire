using System.Threading.Tasks;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
    public interface IQrService
    {
        /// <summary>
        /// Generate QR code and save it to pictures library.
        /// </summary>
        /// <param name="contactName">Name of the contact to save into QR code</param>
        /// /// <returns>Indication whether the QR code generation succeded</returns>
        Task<bool> GenerateQrCode(string contactName);

        /// <summary>
        /// Parse QR code data into Contact object.
        /// </summary>
        /// <param name="qrData">QR code content</param>
        /// <param name="contact">Contact object to load parsed data into</param>
        /// <returns>Indication whether the parse succeded</returns>
        bool ParseQrCode(string qrData, out Contact contact);
    }
}