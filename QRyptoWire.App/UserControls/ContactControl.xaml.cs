﻿using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace QRyptoWire.App.UserControls
{
	public sealed partial class ContactControl
	{
		public ContactControl()
		{
			InitializeComponent();
		}

		public static DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
			typeof(ContactControl), new PropertyMetadata(null, CommandChanged));

		private static void CommandChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			var list = (ListBox)obj;
			list.SelectionChanged += (sender, eventArgs) =>
			{
				var cmd = (ICommand)args.NewValue;
				cmd.Execute(list.SelectedItem);
			};
		}

		public static void SetCommand(DependencyObject obj, ICommand value)
		{
			obj.SetValue(CommandProperty, obj);
		}

		public static ICommand GetCommand(DependencyObject obj)
		{
			return (ICommand)obj.GetValue(CommandProperty);
		}
	}
}