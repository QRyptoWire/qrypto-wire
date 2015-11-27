﻿using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using QRyptoWire.App.WPhone.Utilities;
using QRyptoWire.Core.Services;
using QRyptoWire.Shared.Dto;
using ZXing;

namespace QRyptoWire.App.WPhone.PhoneImplementations
{
	public class QrService : IQrService
    {
        private static class QrElements
        {
            public const int IdIndex = 0;
            public const int NameIndex = 1;
            public const int ModulusIndex = 2;
            public const int ExponentIndex = 3;
            public const char Separator = ';';
            public const int Count = 4;
        }

        private const string QrCodeFileName = "QRyptoWire-ContactCard.jpeg";

        private readonly IEncryptionService _encryptionService;
        private readonly IStorageService _storageService;

        public QrService(IEncryptionService encryptionService, IStorageService storageService)
        {
            _encryptionService = encryptionService;
            _storageService = storageService;
        }

        public async Task GenerateQrCode(string contactName)
        {
            int userId = _storageService.GetUserId();
            string publicKey = _storageService.GetPublicKey();
            Tuple<string, string> pkElements = _encryptionService.DecomposePublicKey(publicKey);

            string contents = ComposeQrData(userId, contactName, pkElements.Item1, pkElements.Item2);

            IBarcodeWriter writer = QrTools.GetQrWriter();

            var matrix = writer.Encode(contents);
            var wb = writer.Write(matrix);

            await PictureLibraryTools.SaveWriteableBitmap(wb, QrCodeFileName);
        }

        public bool ParseQrCode(string qrData, out Contact contact)
        {
            contact = null;

            string[] elements = DecomposeQrData(qrData);

            if (elements?.Length != QrElements.Count)
                return false;

            int userId;
            if (!Int32.TryParse(elements[QrElements.IdIndex], NumberStyles.None, CultureInfo.InvariantCulture, out userId))
                return false;

            string name = elements[QrElements.NameIndex];
            string modulus = elements[QrElements.ModulusIndex];
            string exponent = elements[QrElements.ExponentIndex];

            string publicKey;
            if (!_encryptionService.ComposePublicKey(modulus, exponent, out publicKey))
                return false;

            contact = new Contact() { Name = name, PublicKey = publicKey, UserId = userId };
            return true;
        }

        private string ComposeQrData(int id, string name, string modulus, string exponent)
        {
            string[] elements = new string[QrElements.Count];

            elements[QrElements.IdIndex] = Convert.ToString(id);
            elements[QrElements.NameIndex] = name;
            elements[QrElements.ModulusIndex] = modulus;
            elements[QrElements.ExponentIndex] = exponent;

            return elements.Aggregate((i, j) => i + QrElements.Separator + j);
        }

        private string[] DecomposeQrData(string qrData)
        {
            return qrData?.Split(QrElements.Separator);
        }
    }
}
