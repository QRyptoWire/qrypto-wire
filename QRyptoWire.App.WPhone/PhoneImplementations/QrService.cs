using System;
using QRyptoWire.Core.Services;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.App.WPhone.PhoneImplementations
{
    class QrService : IQrService
    {
        public void GenerateQrCode(string contactName)
        {
            throw new NotImplementedException();
        }

        public bool ParseQrCode(string qrData, out Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
