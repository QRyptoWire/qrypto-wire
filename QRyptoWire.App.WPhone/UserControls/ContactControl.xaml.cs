using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QRyptoWire.App.WPhone.UserControls
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
				if(list.SelectedItem == null)
					return;
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
