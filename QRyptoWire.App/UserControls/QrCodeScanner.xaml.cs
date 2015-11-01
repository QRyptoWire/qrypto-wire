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
using QRyptoWire.App.Annotations;
using QRyptoWire.Shared.Dto;
using QRyptoWire.Core.UserExceptions;
using Panel = Windows.Devices.Enumeration.Panel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace QRyptoWire.App.UserControls
{
    public sealed partial class QrCodeScanner : UserControl
    {
        private MediaCapture _mediaCapture;

        public QrCodeScanner()
        {
            this.InitializeComponent();
        }

        public async Task InitializeAsync()
        {
            await InitializeCameraPreviewAsync();
            await StartCameraPreview();
        }

        private async Task InitializeCameraPreviewAsync()
        {
            var cam = await GetBackCamera();

            if (cam == null)
            {
                cam = await GetFrontCamera();

                if (cam == null)
                {
                    throw new Exception();
                }
            }

            _mediaCapture = new MediaCapture();

            await _mediaCapture.InitializeAsync(new MediaCaptureInitializationSettings()
            {
                VideoDeviceId = cam.Id,
                AudioDeviceId = "",
                PhotoCaptureSource = PhotoCaptureSource.VideoPreview,
                StreamingCaptureMode = StreamingCaptureMode.Video,
            });
        }

        private async Task StartCameraPreview()
        {
            cameraPreview.Source = _mediaCapture;
            await _mediaCapture.StartPreviewAsync();
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

    }
}
