using QRyptoWire.Shared.Dto;

namespace QRyptoWire.Core.Services
{
    public interface IQrService
    {
        byte[] GenerateQrCode(string contactName);
        Contact ParseQrCode(string qrData);
    }
}