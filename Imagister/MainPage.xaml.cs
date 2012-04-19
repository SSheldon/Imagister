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
	}
}