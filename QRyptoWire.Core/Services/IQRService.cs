using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRyptoWire.Core.Services
{
    public interface IQrService
    {
        byte[] GenerateQrCode(string contactName);
        Contact ParseQrCode(string qrData);
    }
}