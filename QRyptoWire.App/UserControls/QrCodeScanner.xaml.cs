using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Windows;
using Windows.Devices.Enumeration;
using Windows.Media;
using Windows.Media.MediaProperties;
using Windows.Storage;
using QRyptoWire.App.Annotations;
using QRyptoWire.Shared.Dto;
using QRyptoWire.Core.CustomExceptions;
using Panel = Windows.Devices.Enumeration.Panel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace QRyptoWire.App.UserControls
{
    public sealed partial class QrCodeScanner : UserControl
    {
        private MediaCapture _mediaCapture;
        private DispatcherTimer _timer;

        private bool IsTimerInitialized => _timer != null;
        private bool IsCameraInitialized => cameraPreview.Source != null;

        private const double ScanIntervalMs = 400.0;

        public QrCodeScanner()
        {
            this.InitializeComponent();
        }

        public async Task StartQrScanAsync()
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

        public async Task StopQrScanAsync()
        {
            _timer.Stop();
            await StopCameraPreviewAsync();
        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(ScanIntervalMs) };
            _timer.Tick += TimerOnTick;
        }

        private async void TimerOnTick(object sender, object o)
        {
            StorageFile tempFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("temp_photo.jpg", CreationCollisionOption.GenerateUniqueName);

            await _mediaCapture.CapturePhotoToStorageFileAsync(ImageEncodingProperties.CreateJpeg(), tempFile);
            await tempFile.DeleteAsync();
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
                if (res.Width*res.Height > currentMax)
                {
                    currentMax = Convert.ToInt32(Width*res.Height);
                    resolutionMax = res;
                }
            }

            await _mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.Photo, resolutionMax);
        }
    }
}
