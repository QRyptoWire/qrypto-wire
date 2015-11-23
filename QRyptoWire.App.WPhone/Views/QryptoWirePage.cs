using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Cirrious.MvvmCross.WindowsPhone.Views;
using Microsoft.Phone.Shell;
using QRyptoWire.App.WPhone.Converters;
using QRyptoWire.Core.ViewModels;

namespace QRyptoWire.App.WPhone.Views
{
	public abstract class QryptoWirePage : MvxPhonePage
	{
		protected QryptoWirePage()
		{
			
            Loaded += (sender, args) =>
            {
                SystemTray.IsVisible = false;

				var dc = DataContext as QryptoViewModel;
				if (dc != null)
				{
					var pageContent = Content as FrameworkElement;
						if(pageContent == null)
							return;

					var rootGrid = new Grid
					{
						RowDefinitions = { new RowDefinition { Height = GridLength.Auto } }
					};

					//var binding = new Binding()
					//{
					//	Source = dc.Working,
					//	Converter = new BoolToVisibilityConverter(),
					//	FallbackValue = false
					//};

					var overlay = new WorkingOverlay();
					Grid.SetRow(overlay, 1);
					Grid.SetRow(pageContent, 1);
					//BindingOperations.SetBinding(overlay, VisibilityProperty, binding);

					Content = rootGrid;

					rootGrid.Children.Add(pageContent);
					rootGrid.Children.Add(overlay);
				}
			};
		}
	}
}
