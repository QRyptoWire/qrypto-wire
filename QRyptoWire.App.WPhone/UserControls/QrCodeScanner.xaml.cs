using System;
using System.Linq;
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
		private PhotoCamera _photoCamera;
		private DispatcherTimer _timer;
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
		public void Start()
		{
			InitializeCameraPreview();
		}

		/// <summary>
		/// Stop camera preview and QR code scanner.
		/// </summary>
		public void Stop()
		{
			if (_timer.IsEnabled)
			{
				_timer.Stop();
			}
			_photoCamera?.Dispose();
		}

		private void InitializeCameraPreview()
		{
			if (PhotoCamera.IsCameraTypeSupported(CameraType.Primary))
			{
				_photoCamera = new Microsoft.Devices.PhotoCamera(CameraType.Primary);
			}
			else if (PhotoCamera.IsCameraTypeSupported(CameraType.FrontFacing))
			{
				_photoCamera = new Microsoft.Devices.PhotoCamera(CameraType.Primary);
			}
			else
			{
				throw new CameraNotFoundException("No camera is present");
			}

			_photoCamera.Initialized += PhotoCamera_OnInitialized;
			_photoCamera.CaptureImageAvailable += PhotoCamera_OnImageAvailable;
		}

		void PhotoCamera_OnInitialized(object sender, CameraOperationCompletedEventArgs e)
		{
			if (e.Succeeded)
			{
				SetCameraToMaxResolution();
				ViewFinderBrush.SetSource(_photoCamera);
				InitializeTimer();
				StartTimer();
			}
		}

		void PhotoCamera_OnImageAvailable(object sender, ContentReadyEventArgs e)
		{
			BitmapImage bmpImg = new BitmapImage();
			bmpImg.SetSource(e.ImageStream);
			WriteableBitmap wb = new WriteableBitmap(bmpImg);

			string text = DecodeQrCode(wb);
			if (text != null)
			{
				QrCodeDetected(text);
			}
		}

		private void InitializeTimer()
		{
			_timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(ScanIntervalMs) };
			_timer.Tick += TimerOnTick;
		}

		private void StartTimer()
		{
			if (!_timer.IsEnabled)
			{
				_timer.Start();
			}
		}

		private void TimerOnTick(object sender, object o)
		{
			_photoCamera.CaptureImage();
		}

		private string DecodeQrCode(WriteableBitmap wb)
		{
			IBarcodeReader reader = QrTools.GetQrReader();
			var result = reader.Decode(wb);
			return result?.Text;
		}

		private void SetCameraToMaxResolution()
		{
			_photoCamera.Resolution = GetMaxCameraResolution();
		}

		private System.Windows.Size GetMaxCameraResolution()
		{
			return _photoCamera.AvailableResolutions.OrderByDescending(size => size.Height * size.Width).First();
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