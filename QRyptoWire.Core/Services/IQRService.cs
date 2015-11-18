using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
    public interface IQrService
    {
        void GenerateQrCode(string contactName);
        bool ParseQrCode(string qrData, out Contact contact);
    }
}