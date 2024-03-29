﻿using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Xna.Framework.Media;

namespace Imagister
{
	/// <summary>
	/// MainPage is the main page of the Imagister app,
	/// where images are displayed and manipulated.
	/// </summary>
	public partial class MainPage : PhoneApplicationPage
	{
		private PreviewBitmap bmp;
		private PhotoChooserTask chooser;

		/// <summary>
		/// Constructs a MainPage.
		/// </summary>
		public MainPage()
		{
			InitializeComponent();

			bmp = (App.Current as App).Bmp;
			DataContext = bmp;

			chooser = new PhotoChooserTask();
			chooser.ShowCamera = true;
			chooser.Completed += PhotoChosen;
		}

		/// <summary>
		/// Called when a photo is chosen from the PhotoChooser task.
		/// </summary>
		private void PhotoChosen(object sender, PhotoResult e)
		{
			if (e.TaskResult == TaskResult.OK)
			{
				bmp.Load(e.ChosenPhoto);
			}
		}

		/// <summary>
		/// Called when this MainPage becomes the active page in a frame.
		/// </summary>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			if (!bmp.IsLoaded)
			{
				Stream stream;
				var query = this.NavigationContext.QueryString;
				if (query.ContainsKey("token"))
				{
					MediaLibrary lib = new MediaLibrary();
					Picture pic = lib.GetPictureFromToken(query["token"]);
					stream = pic.GetImage();
				}
				else
				{
					Uri uri = new Uri("Images/lenna.jpg", UriKind.Relative);
					stream = Application.GetResourceStream(uri).Stream;
				}
				bmp.Load(stream);
			}
			base.OnNavigatedTo(e);
		}

		#region Transform Handlers
		private void RotateRightTap(object sender, GestureEventArgs e)
		{
			bmp.Manipulate(img => img.RotateRight());
		}

		private void RotateLeftTap(object sender, GestureEventArgs e)
		{
			bmp.Manipulate(img => img.RotateLeft());
		}

		private void RotateDownTap(object sender, GestureEventArgs e)
		{
			bmp.Manipulate(img => img.RotateDown());
		}

		private void FlipHorizontalTap(object sender, GestureEventArgs e)
		{
			bmp.Manipulate(img => img.FlipHorizontal());
		}

		private void FlipVerticalTap(object sender, GestureEventArgs e)
		{
			bmp.Manipulate(img => img.FlipVertical());
		}
		#endregion

		#region Filter Handlers
		private void GrayscaleTap(object sender, GestureEventArgs e)
		{
			bmp.Manipulate(img => img.Apply(Shaders.Grayscale));
		}

		private void InvertTap(object sender, GestureEventArgs e)
		{
			bmp.Manipulate(img => img.Apply(Shaders.Invert));
		}

		private void SepiaTap(object sender, GestureEventArgs e)
		{
			bmp.Manipulate(img => img.Apply(Shaders.Sepia));
		}

		private void PosterizeTap(object sender, GestureEventArgs e)
		{
			bmp.Manipulate(img => img.Apply(Shaders.Posterize(4)));
		}

		private void DitherTap(object sender, GestureEventArgs e)
		{
			bmp.Manipulate(img => Ditherer.Dither(img, 4));
		}
		#endregion

		#region AppBar Handlers
		private void RevertClick(object sender, EventArgs e)
		{
			bmp.Load();
		}

		private void OpenClick(object sender, EventArgs e)
		{
			chooser.Show();
		}

		private void SaveClick(object sender, EventArgs e)
		{
			Uri uri = new Uri("/SavePage.xaml", UriKind.Relative);
			NavigationService.Navigate(uri);
		}
		#endregion
	}
}