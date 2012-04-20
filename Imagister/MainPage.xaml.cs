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
		private ImageInfo info;

		// Constructor
		public MainPage()
		{
			InitializeComponent();
			info = new ImageUri(new Uri("Images/lenna.jpg", UriKind.Relative));
			bmp = info.Bitmap;
			imageControl.Source = bmp.SourceImage;
		}

		#region Transform Handlers
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
		#endregion

		#region Filter Handlers
		private void GrayscaleTap(object sender, GestureEventArgs e)
		{
			bmp.Apply(Shaders.Grayscale);
			imageControl.Source = bmp.SourceImage;
		}

		private void InvertTap(object sender, GestureEventArgs e)
		{
			bmp.Apply(Shaders.Invert);
			imageControl.Source = bmp.SourceImage;
		}

		private void SepiaTap(object sender, GestureEventArgs e)
		{
			bmp.Apply(Shaders.Sepia);
			imageControl.Source = bmp.SourceImage;
		}

		private void PosterizeTap(object sender, GestureEventArgs e)
		{
			bmp.Apply(Shaders.Posterize(4));
			imageControl.Source = bmp.SourceImage;
		}

		private void DitherTap(object sender, GestureEventArgs e)
		{
			Ditherer.Dither(bmp, 4);
			imageControl.Source = bmp.SourceImage;
		}
		#endregion

		#region AppBar Handlers
		private void RevertClick(object sender, EventArgs e)
		{
			bmp = info.Bitmap;
			imageControl.Source = bmp.SourceImage;
		}

		private void OpenClick(object sender, EventArgs e)
		{
		}

		private void SaveClick(object sender, EventArgs e)
		{
		}
		#endregion
	}
}