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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace QRyptoWire.App.UserControls
{
    public sealed partial class QrCodeScanner : UserControl
    {

        public QrCodeScanner()
        {
            this.InitializeComponent();
        }

    }
}
