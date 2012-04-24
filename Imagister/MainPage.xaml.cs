using System;
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
	public partial class MainPage : PhoneApplicationPage
	{
		private PreviewBitmap bmp;
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
				SetupImage(stream);
			}
		}

		private void PhotoChosen(object sender, PhotoResult e)
		{
			SetupImage(e.ChosenPhoto);
		}

		private void SetupImage(Stream stream)
		{
			bmp = new PreviewBitmap(stream);
			imageControl.DataContext = bmp;
			bmp.Load();
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
		}
		#endregion
	}
}