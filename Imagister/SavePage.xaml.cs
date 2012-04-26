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

		public SavePage()
		{
			InitializeComponent();

			bmp = (App.Current as App).Bmp;
			this.DataContext = bmp.Dimensions;
		}

		private void SaveClick(object sender, EventArgs e)
		{
		}

		private void CancelClick(object sender, EventArgs e)
		{
			NavigationService.GoBack();
		}
	}
}
