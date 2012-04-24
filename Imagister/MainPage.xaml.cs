﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using Microsoft.Phone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Imagister
{
	public partial class MainPage : PhoneApplicationPage
	{
		private ManipulableBitmap bmp;
		private PhotoChooserTask chooser;

		// Constructor
		public MainPage()
		{
			InitializeComponent();

			chooser = new PhotoChooserTask();
			chooser.ShowCamera = true;
			chooser.Completed += new EventHandler<PhotoResult>(PhotoChosen);
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			if (bmp == null)
			{
				ImageInfo info;
				var query = this.NavigationContext.QueryString;
				if (query.ContainsKey("token"))
				{
					info = new ImageToken(query["token"]);
				}
				else
				{
					Uri uri = new Uri("Images/lenna.jpg", UriKind.Relative);
					info = new ImageUri(uri);
				}
				bmp = new ManipulableBitmap(info.Stream);
				imageControl.Source = bmp.SourceImage;
			}
		}

		private void PhotoChosen(object sender, PhotoResult e)
		{
			ImageInfo info = new ImageResult(e);
			bmp = new ManipulableBitmap(info.Stream);
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
		}

		private void OpenClick(object sender, EventArgs e)
		{
			chooser.Show();
		}

		private void SaveClick(object sender, EventArgs e)
		{
		}
		#endregion
	}
}