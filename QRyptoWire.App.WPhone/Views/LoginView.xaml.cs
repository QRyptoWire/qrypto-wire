using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QRyptoWire.App.WPhone.Views
{
    public partial class LoginView
    {
        public LoginView()
        {
            InitializeComponent();
        }

	    public static readonly DependencyProperty DynamicTextBoxProperty =
		    DependencyProperty.RegisterAttached("DynamicTextBox", typeof (bool), typeof (TextBox),
			    new PropertyMetadata(default(bool), PropertyChangedCallback));

		public static void SetCommand(DependencyObject obj, bool value)
		{
			obj.SetValue(DynamicTextBoxProperty, obj);
		}

		public static bool GetCommand(DependencyObject obj)
		{
			return (bool)obj.GetValue(DynamicTextBoxProperty);
		}

		private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
	    {
		    throw new NotImplementedException();
	    }
    }
}