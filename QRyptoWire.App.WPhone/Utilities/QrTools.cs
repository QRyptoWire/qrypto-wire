using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace QRyptoWire.App.WPhone.Utilities
{
    public class QrTools
    {
        private const int QrCodeWidth = 300;
        private const int QrCodeHeight = 300;
        private const int QrCodeMargin = 2;

        public static IBarcodeWriter GetQrWriter()
        {
            return new BarcodeWriter()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions()
                {
                    Width = QrCodeWidth,
                    Height = QrCodeHeight,
                    Margin = QrCodeMargin,
                    PureBarcode = true,
                    ErrorCorrection = ErrorCorrectionLevel.M,
                }
            };
        }

        public static IBarcodeReader GetQrReader()
        {
            return new BarcodeReader()
            {
                Options = new DecodingOptions()
                {
                    PossibleFormats = new[] { BarcodeFormat.QR_CODE },
                    PureBarcode = false,
                    TryHarder = true
                }
            };
        }
    }
}
