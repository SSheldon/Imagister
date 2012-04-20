using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using Microsoft.Phone;
using Microsoft.Phone.Controls;

namespace Imagister
{
	public partial class MainPage : PhoneApplicationPage
	{
		private ManipulableBitmap bmp;

		// Constructor
		public MainPage()
		{
			InitializeComponent();
			LoadImage();
		}

		public void LoadImage()
		{
			Uri uri = new Uri("Images/lenna.jpg", UriKind.Relative);
			StreamResourceInfo sri = Application.GetResourceStream(uri);
			bmp = new ManipulableBitmap(sri.Stream);

			imageControl.Source = bmp.SourceImage;
		}

		private void RotateRightTap(object sender, GestureEventArgs e)
		{
			bmp.RotateRight();
			imageControl.Source = bmp.SourceImage;
		}

		private void RotateLeftTap(object sender, GestureEventArgs e)
		{
			bmp.RotateLeft();
			imageControl.Source = bmp.SourceImage;
		}

		private void RotateDownTap(object sender, GestureEventArgs e)
		{
			bmp.RotateDown();
			imageControl.Source = bmp.SourceImage;
		}

		private void FlipHorizontalTap(object sender, GestureEventArgs e)
		{
			bmp.FlipHorizontal();
			imageControl.Source = bmp.SourceImage;
		}

		private void FlipVerticalTap(object sender, GestureEventArgs e)
		{
			bmp.FlipVertical();
			imageControl.Source = bmp.SourceImage;
		}

		private void OpenClick(object sender, EventArgs e)
		{
		}

		private void SaveClick(object sender, EventArgs e)
		{
		}
	}
}