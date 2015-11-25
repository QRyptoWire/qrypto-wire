using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Devices;
using QRyptoWire.App.WPhone.Utilities;
using QRyptoWire.Core.CustomExceptions;
using ZXing;

namespace QRyptoWire.App.WPhone.UserControls
{
    public sealed partial class QrCodeScanner
    {
        #region ScannerImplementation
        private PhotoCamera _cam;
        private DispatcherTimer _scanningTimer;
        private DispatcherTimer _focusingTimer;
        private readonly TimeSpan _scanningInterval;
        private readonly TimeSpan _focusingInterval;

        public delegate void QrCodeDetectedEventHandler(string text);

        public event QrCodeDetectedEventHandler QrCodeDetected;

        public QrCodeScanner()
        {
            InitializeComponent();
            _scanningInterval = TimeSpan.FromMilliseconds(400);
            _focusingInterval = TimeSpan.FromMilliseconds(40);
        }

        public void Start()
        {
            // after the camera is initialized it will automatically start focusing and scanning tasks
            StartCamera();
        }

        public void Stop()
        {
            StopScanning();
            StopFocusing();
            StopCamera();
        }


        private void StartCamera()
        {
            if (!IsCameraAvailable())
                throw new CameraNotFoundException("Camera device not available");

            _cam = new PhotoCamera(GetAvailableCameraType());
            _cam.Initialized += CamOnInitialized;
            ViewFinderBrush.SetSource(_cam);
        }

        private void StopCamera()
        {
            _cam.Initialized -= CamOnInitialized;
            _cam.Dispose();
        }

        private bool IsCameraAvailable()
        {
            return Camera.IsCameraTypeSupported(CameraType.Primary) || Camera.IsCameraTypeSupported(CameraType.FrontFacing);
        }

        private CameraType GetAvailableCameraType()
        {
            if (Camera.IsCameraTypeSupported(CameraType.Primary))
                return CameraType.Primary;
            return CameraType.FrontFacing;
        }

        private void CamOnInitialized(object sender, CameraOperationCompletedEventArgs eventArgs)
        {
            Dispatcher.BeginInvoke(() =>
            {
                if (eventArgs.Succeeded)
                {
                    StartFocusing();
                    StartScanning();
                }
            });
        }

        private void StartScanning()
        {
            _scanningTimer = new DispatcherTimer() { Interval = _scanningInterval };
            _scanningTimer.Tick += ScanningForQrCode;
            _scanningTimer.Start();
        }

        private void StopScanning()
        {
            _scanningTimer.Tick -= ScanningForQrCode;
            _scanningTimer.Stop();
        }

        private void ScanningForQrCode(object sender, EventArgs eventArgs)
        {
            // get camera preview
            byte[] pixelData = new byte[(int)_cam.PreviewResolution.Width * (int)_cam.PreviewResolution.Height];
            _cam.GetPreviewBufferY(pixelData);
            WriteableBitmap wb = new WriteableBitmap((int)_cam.PreviewResolution.Width, (int)_cam.PreviewResolution.Height);
            wb.FromByteArray(pixelData);

            // process image
            string text = DecodeQrCode(wb);
            if (text != null)
                QrCodeDetected?.Invoke(text);
        }

        private string DecodeQrCode(WriteableBitmap wb)
        {
            IBarcodeReader reader = QrTools.GetQrReader();
            var result = reader.Decode(wb);
            return result?.Text;
        }

        private void StartFocusing()
        {
            _focusingTimer = new DispatcherTimer() { Interval = _focusingInterval };
            _focusingTimer.Tick += FocusingCamera;
            _focusingTimer.Start();
        }

        private void StopFocusing()
        {
            _focusingTimer.Tick -= FocusingCamera;
            _focusingTimer.Stop();
        }

        private void FocusingCamera(object sender, EventArgs eventArgs)
        {
            if (_cam.IsFocusSupported)
                _cam.Focus();
        }
        #endregion ScannerImplementation

        #region OnDetectedCommand
        public static readonly DependencyProperty OnDetectedCommandProperty = DependencyProperty.Register("OnDetectedCommand", typeof(ICommand), typeof(QrCodeScanner), new PropertyMetadata(null, OnDetectedChanged));

        public ICommand OnDetectedCommand
        {
            get { return (ICommand)GetValue(OnDetectedCommandProperty); }
            set { SetValue(OnDetectedCommandProperty, value); }
        }

        private static void OnDetectedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var sender = obj as QrCodeScanner;
            if (sender == null)
                return;
            sender.QrCodeDetected += text =>
            {
                var cmd = sender.OnDetectedCommand;
                cmd.Execute(text);
            };
            sender.Start();
        }
        #endregion OnDetectedCommand
    }
}