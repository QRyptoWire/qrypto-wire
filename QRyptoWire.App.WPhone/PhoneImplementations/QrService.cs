using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Windows.Storage;
using QRyptoWire.Core.Services;
using QRyptoWire.Shared.Dto;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

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

        private const int QrCodeWidth = 300;
        private const int QrCodeHeight = 300;
        private const int QrCodeMargin = 2;

        private const string QrCodeFileName = "QRyptoWire-ContactCard.png";

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

            IBarcodeWriter writer = new BarcodeWriter()
            {
                Encoder = new QRCodeWriter(),
                Options = new QrCodeEncodingOptions()
                {
                    Width = QrCodeWidth,
                    Height = QrCodeHeight,
                    Margin = QrCodeMargin,
                    PureBarcode = true,
                    ErrorCorrection = ErrorCorrectionLevel.M,
                }
            };

            var matrix = writer.Encode(contents);
            var bmp = writer.Write(matrix);

            await SaveWriteableBitmapToPictureLibrary(bmp);
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

        private string ComposeQrData(int id, string name, string modulus, string exponent)
        {
            return id + DataSeparator + name + DataSeparator + modulus + DataSeparator + exponent;
        }

        private async Task SaveWriteableBitmapToPictureLibrary(WriteableBitmap bmp)
        {
            var storageFolder = KnownFolders.SavedPictures;
            var file = await storageFolder.CreateFileAsync(QrCodeFileName, CreationCollisionOption.GenerateUniqueName);

            using (var stream = await file.OpenStreamForWriteAsync())
            {
                bmp.SaveJpeg(stream, bmp.PixelWidth, bmp.PixelHeight, 0, 100);
            }
        }
    }
}
