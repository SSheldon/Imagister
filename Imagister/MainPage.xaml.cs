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
		private ManipulableImage img;
		private WriteableBitmap bmp;

		// Constructor
		public MainPage()
		{
			InitializeComponent();
			LoadImage();
		}

		public void LoadImage()
		{
			Uri uri = new Uri("/Imagister;component/test.jpg", UriKind.Relative);
			StreamResourceInfo sri = Application.GetResourceStream(uri);
			bmp = PictureDecoder.DecodeJpeg(sri.Stream);
			img = new ManipulableImage(bmp.PixelHeight, bmp.PixelWidth, bmp.Pixels);

			imageControl.Source = bmp;
		}

		private void RotateRightTap(object sender, GestureEventArgs e)
		{
			bmp = new WriteableBitmap(bmp.PixelHeight, bmp.PixelWidth);
			img.RotateRight(bmp.Pixels);
			imageControl.Source = bmp;
		}

		private void RotateLeftTap(object sender, GestureEventArgs e)
		{
			bmp = new WriteableBitmap(bmp.PixelHeight, bmp.PixelWidth);
			img.RotateLeft(bmp.Pixels);
			imageControl.Source = bmp;
		}

		private void RotateDownTap(object sender, GestureEventArgs e)
		{
			img.RotateDown();
			imageControl.Source = bmp;
		}

		private void FlipHorizontalTap(object sender, GestureEventArgs e)
		{
			img.FlipHorizontal();
			imageControl.Source = bmp;
		}

		private void FlipVerticalTap(object sender, GestureEventArgs e)
		{
			img.FlipVertical();
			imageControl.Source = bmp;
		}

		private void OpenClick(object sender, EventArgs e)
		{
		}

		private void SaveClick(object sender, EventArgs e)
		{
		}
	}
}