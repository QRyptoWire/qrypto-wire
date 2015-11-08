using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.Devices.Enumeration;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using QRyptoWire.Core.CustomExceptions;
using ZXing;
using ZXing.Common;
using Panel = Windows.Devices.Enumeration.Panel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace QRyptoWire.App.UserControls
{
    public sealed partial class QrCodeScanner
    {
		#region ScannerImplementation
        private MediaCapture _mediaCapture;
        private DispatcherTimer _timer;

        private bool IsTimerInitialized => _timer != null;
        private bool IsCameraInitialized => cameraPreview.Source != null;

        private int _deviceResolutionWidthPx = 0;
        private int _deviceResolutionHeightPx = 0;

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
            if (!IsCameraInitialized)
            {
                await InitializeCameraPreviewAsync();
            }

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
            _timer.Stop();
            await StopCameraPreviewAsync();
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

            cameraPreview.Source = _mediaCapture;
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

                using (var fileStream = await tmpFile.OpenReadAsync())
                {
                    WriteableBitmap bmp = new WriteableBitmap(_deviceResolutionWidthPx, _deviceResolutionHeightPx);
                    await bmp.SetSourceAsync(fileStream);

                    string text = DecodeQrCode(bmp);
                    if (text != null)
                    {
                        QrCodeDetected(text);
                    }
                }
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
                        PossibleFormats = new BarcodeFormat[] { BarcodeFormat.QR_CODE },
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
            VideoEncodingProperties resolutionMax = null;
            int currentMax = 0;
            var availableResolutions = _mediaCapture.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.Photo);

            foreach (VideoEncodingProperties res in availableResolutions)
            {
                if (res.Width * res.Height > currentMax)
                {
                    _deviceResolutionWidthPx = Convert.ToInt32(res.Width);
                    _deviceResolutionHeightPx = Convert.ToInt32(res.Height);
                    currentMax = Convert.ToInt32(Width * res.Height);
                    resolutionMax = res;
                }
            }

            await _mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.Photo, resolutionMax);
        }
		#endregion ScannerImplementation

		#region OnDetectedCommand
	    public static readonly DependencyProperty OnDetectedCommandProperty =
		    DependencyProperty.Register("OnDetectedCommand", typeof (ICommand), typeof (QrCodeScanner),
			    new PropertyMetadata(null, OnDetectedChanged));

	    public ICommand OnDetectedCommand
	    {
		    get { return (ICommand) GetValue(OnDetectedCommandProperty); }
			set { SetValue(OnDetectedCommandProperty, value);}
	    }

	    private static void OnDetectedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
	    {
		    var sender = obj as QrCodeScanner;
			if(sender == null)
				return;
		    sender.QrCodeDetected += text =>
		    {
			    var cmd = sender.OnDetectedCommand;
			    cmd.Execute(text);
		    };
	    }
	    #endregion OnDetectedCommand
	}
}