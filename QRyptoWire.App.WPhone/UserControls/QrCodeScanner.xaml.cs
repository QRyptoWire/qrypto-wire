using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Microsoft.Devices;
using QRyptoWire.Core.CustomExceptions;
using ZXing;
using ZXing.Common;
using Panel = Windows.Devices.Enumeration.Panel;

namespace QRyptoWire.App.WPhone.UserControls
{
    public sealed partial class QrCodeScanner
    {
        #region ScannerImplementation
        private MediaCapture _mediaCapture;
        private DispatcherTimer _timer;
        private bool IsTimerInitialized => _timer != null;
        private const double ScanIntervalMs = 400.0;

        public delegate void QrCodeDetectedEventHandler(string text);

        /// <summary>
        /// Event that is fired when QR code is detected by camera.
        /// </summary>
        public event QrCodeDetectedEventHandler QrCodeDetected = delegate { };

        public QrCodeScanner()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Start camera preview and QR code scanner.
        /// </summary>
        /// <returns>Task</returns>
        public async Task StartAsync()
        {
            await InitializeCameraPreviewAsync();
            await StartCameraPreviewAsync();

            if (!IsTimerInitialized)
            {
                InitializeTimer();
            }

            _timer.Start();
        }

        /// <summary>
        /// Stop camera preview and QR code scanner.
        /// </summary>
        /// <returns>Task</returns>
        public async Task StopAsync()
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                await StopCameraPreviewAsync();
            }
           _mediaCapture?.Dispose();
        }

        private async Task InitializeCameraPreviewAsync()
        {
            var cam = await GetBackCamera();

            if (cam == null)
            {
                cam = await GetFrontCamera();

                if (cam == null)
                {
                    throw new CameraNotFoundException("No camera is present");
                }
            }

            _mediaCapture = new MediaCapture();

            await _mediaCapture.InitializeAsync(new MediaCaptureInitializationSettings()
            {
                VideoDeviceId = cam.Id,
                PhotoCaptureSource = PhotoCaptureSource.VideoPreview,
                StreamingCaptureMode = StreamingCaptureMode.Video
            });

            await SetCameraToMaxResolution();

            ViewFinderBrush.SetSource(_mediaCapture);
        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(ScanIntervalMs) };
            _timer.Tick += TimerOnTick;
        }

        private async void TimerOnTick(object sender, object o)
        {
            try
            {
                StorageFile tmpFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("temp_photo.jpg", CreationCollisionOption.GenerateUniqueName);

                await _mediaCapture.CapturePhotoToStorageFileAsync(ImageEncodingProperties.CreateJpeg(), tmpFile);

                using (var fileStream = await tmpFile.OpenStreamForReadAsync())
                {
                    var bmpImg = new BitmapImage { CreateOptions = BitmapCreateOptions.None };
                    bmpImg.SetSource(fileStream);
                    WriteableBitmap wbmp = new WriteableBitmap((BitmapSource)bmpImg);

                    string text = DecodeQrCode(wbmp);
                    if (text != null)
                    {
                        QrCodeDetected(text);
                    }
                }

                await tmpFile.DeleteAsync();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private string DecodeQrCode(WriteableBitmap bmp)
        {
            IBarcodeReader reader = new BarcodeReader()
            {              
                Options =
                    new DecodingOptions()
                    {
                        PossibleFormats = new[] { BarcodeFormat.QR_CODE },
                        PureBarcode = false,
                        TryHarder = true
                    }
            };
            var result = reader.Decode(bmp);
            return result?.Text;
        }

        private async Task StartCameraPreviewAsync()
        {
            await _mediaCapture.StartPreviewAsync();
        }

        private async Task StopCameraPreviewAsync()
        {
            await _mediaCapture.StopPreviewAsync();
        }

        private async Task<DeviceInformation> GetBackCamera()
        {
            var camList = await GetCameraDevices();

            return (from cam in camList
                    where cam.EnclosureLocation != null && cam.EnclosureLocation.Panel == Panel.Back
                    select cam).FirstOrDefault();

        }

        private async Task<DeviceInformation> GetFrontCamera()
        {
            var camList = await GetCameraDevices();

            return (from cam in camList
                    where cam.EnclosureLocation != null && cam.EnclosureLocation.Panel == Panel.Front
                    select cam).FirstOrDefault();
        }

        private async Task<DeviceInformationCollection> GetCameraDevices()
        {
            return await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
        }

        private async Task SetCameraToMaxResolution()
        {
            var availableResolutions = _mediaCapture.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.Photo);

            var resolutionMax = (VideoEncodingProperties)
                availableResolutions.OrderByDescending(
                    r => ((VideoEncodingProperties) r).Width*((VideoEncodingProperties) r).Height).FirstOrDefault();

            if (resolutionMax == null) return;

            await _mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.Photo, resolutionMax);
        }
        #endregion ScannerImplementation


        #region OnDetectedCommand
        public static readonly DependencyProperty OnDetectedCommandProperty =
         DependencyProperty.Register("OnDetectedCommand", typeof(ICommand), typeof(QrCodeScanner),
             new PropertyMetadata(null, OnDetectedChanged));

        public ICommand OnDetectedCommand
        {
            get { return (ICommand)GetValue(OnDetectedCommandProperty); }
            set { SetValue(OnDetectedCommandProperty, value); }
        }

        private async static void OnDetectedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var sender = obj as QrCodeScanner;
            if (sender == null)
                return;
            sender.QrCodeDetected += text =>
            {
                var cmd = sender.OnDetectedCommand;
                cmd.Execute(text);
            };
            await sender.StartAsync();
        }
        #endregion OnDetectedCommand
    }
}