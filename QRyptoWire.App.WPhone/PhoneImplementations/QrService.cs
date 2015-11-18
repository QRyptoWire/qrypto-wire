using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using QRyptoWire.Core.Services;
using QRyptoWire.Shared.Dto;

namespace QRyptoWire.App.WPhone.PhoneImplementations
{
    class QrService : IQrService
    {
        private static class ElementIndex
        {
            public const int Id = 0;
            public const int Name = 1;
            public const int Modulus = 2;
            public const int Exponent = 3;
        }

        private const char DataSeparator = ';';
        private const int ElementsCount = 4;

        private readonly IEncryptionService _encryptionService;

        public QrService(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        public void GenerateQrCode(string contactName)
        {
            throw new NotImplementedException();
        }

        public bool ParseQrCode(string qrData, out Contact contact)
        {
            contact = null;

            string[] elements = qrData?.Split(DataSeparator);

            if (elements?.Length != ElementsCount)
                return false;

            int userId;
            if (!Int32.TryParse(elements[ElementIndex.Id], NumberStyles.None, CultureInfo.InvariantCulture, out userId))
                return false;

            string name = elements[ElementIndex.Name];
            string modulus = elements[ElementIndex.Modulus];
            string exponent = elements[ElementIndex.Exponent];

            string publicKey;
            if (!_encryptionService.ComposePublicKey(modulus, exponent, out publicKey))
                return false;

            contact = new Contact() {Name = name, PublicKey = publicKey, UserId = userId};
            return true;
        }
    }
}
