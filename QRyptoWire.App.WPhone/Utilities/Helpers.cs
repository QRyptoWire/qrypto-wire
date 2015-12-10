using System.Windows;
using System.Windows.Controls;

namespace QRyptoWire.App.WPhone.Utilities
{
	public static class Helpers
	{
		#region DynamicTextBoxProperty
		public static readonly DependencyProperty DynamicTextBoxProperty =
			DependencyProperty.RegisterAttached("DynamicTextBox", typeof (bool), typeof (Helpers),
				new PropertyMetadata(default(bool), IsDynamicChanged));

		private static void IsDynamicChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			if (!(bool) args.NewValue)
				return;
			var tbox = (TextBox) obj;
			tbox.TextChanged += (sender, eventArgs) =>
			{
				if (tbox.Text.Length >= 20)
					return;
				tbox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
			};
		}

		public static bool GetDynamicTextBox(TextBox textBox)
		{
			return (bool)textBox.GetValue(DynamicTextBoxProperty);
		}

		public static void SetDynamicTextBox(TextBox textBox, bool value)
		{
			textBox.SetValue(DynamicTextBoxProperty, value);
		}
		#endregion

		#region DynamicPasswordBoxProperty
		public static readonly DependencyProperty DynamicPasswordBoxProperty =
			DependencyProperty.RegisterAttached("DynamicPasswordBox", typeof(bool), typeof(Helpers),
				new PropertyMetadata(default(bool), IsDynamicPasswordChanged));

		private static void IsDynamicPasswordChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			if (!(bool)args.NewValue)
				return;
			var pbox = (PasswordBox)obj;
			pbox.PasswordChanged += (sender, eventArgs) =>
			{
				pbox.GetBindingExpression(PasswordBox.PasswordProperty).UpdateSource();
			};
		}

		public static bool GetDynamicPasswordBox(PasswordBox textBox)
		{
			return (bool)textBox.GetValue(DynamicPasswordBoxProperty);
		}

		public static void SetDynamicPasswordBox(PasswordBox textBox, bool value)
		{
			textBox.SetValue(DynamicPasswordBoxProperty, value);
		}
		#endregion
	}
}
