using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Imagister
{
	public partial class SavePage : PhoneApplicationPage
	{
		private PreviewBitmap bmp;
		private Dimensions dim;

		public SavePage()
		{
			InitializeComponent();

			bmp = (App.Current as App).Bmp;
			dim = bmp.Dimensions;
			this.DataContext = dim;
		}

		private void OrigSizeClick(object sender, RoutedEventArgs e)
		{
			dim.Scale = 1.0;
		}

		private void MmsSizeClick(object sender, RoutedEventArgs e)
		{
			dim.Max = 480;
		}

		private void SaveClick(object sender, EventArgs e)
		{
			bmp.Save(dim);
			NavigationService.GoBack();
		}

		private void CancelClick(object sender, EventArgs e)
		{
			NavigationService.GoBack();
		}
	}
}
