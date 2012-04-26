using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone;
using Microsoft.Xna.Framework.Media;

namespace Imagister
{
	/// <summary>
	/// Represents a preview of a ManipulableBitmap.
	/// </summary>
	public class PreviewBitmap : INotifyPropertyChanged
	{
		private ImageStore store;
		private ManipulableBitmap image;

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Indicates whether this PreviewBitmap is loaded.
		/// </summary>
		public bool IsLoaded
		{
			get { return store != null && image != null; }
		}

		/// <summary>
		/// Gets an ImageSource for a preview of this PreviewBitmap.
		/// </summary>
		public ImageSource PreviewSource
		{
			get
			{
				return (image != null ? image.SourceImage : null);
			}
		}

		/// <summary>
		/// Gets the Dimensions of this PreviewBitmap's image.
		/// </summary>
		public Dimensions Dimensions
		{
			get
			{
				return (image != null ?
					new Dimensions(image.Width, image.Height) : null);
			}
		}

		/// <summary>
		/// Gets this PreviewBitmap's JPEG source Stream.
		/// </summary>
		private Stream StreamSource
		{
			get
			{
				return store.OpenFile("source.jpg");
			}
		}

		/// <summary>
		/// Creates this PreviewBitmap's source JPEG.
		/// </summary>
		/// <returns>The created JPEG's stream.</returns>
		private Stream CreateSource()
		{
			store = new ImageStore();
			return store.CreateFile("source.jpg");
		}

		/// <summary>
		/// Deletes this PreviewBitmap's source JPEG.
		/// </summary>
		private void DeleteSource()
		{
			store.Delete();
			store = null;
		}

		/// <summary>
		/// Raises the PropertyChanged event to notify listeners that
		/// this PreviewBitmap's image has changed.
		/// </summary>
		private void NotifyImageChanged()
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs("PreviewSource"));
			}
		}

		/// <summary>
		/// Loads this PreviewBitmap from a JPEG source.
		/// </summary>
		/// <param name="stream">The JPEG stream to load.</param>
		public void Load(Stream stream)
		{
			//remove old source JPEG if it exists
			if (store != null) DeleteSource();
			using (Stream dest = CreateSource())
			{
				stream.CopyTo(dest);
			}
			Load();
		}

		/// <summary>
		/// Loads this PreviewBitmap from its JPEG source.
		/// </summary>
		public void Load()
		{
			WriteableBitmap bmp;
			using (Stream source = StreamSource)
			{
				bmp = PictureDecoder.DecodeJpeg(source);
			}
			image = new ManipulableBitmap(bmp);
			NotifyImageChanged();
		}

		/// <summary>
		/// Unloads this PreviewBitmap and deletes its JPEG source.
		/// </summary>
		public void Unload()
		{
			DeleteSource();
			image = null;
			NotifyImageChanged();
		}

		/// <summary>
		/// Manipulates this PreviewBitmap's underlying ManipulableBitmap.
		/// </summary>
		/// <param name="manipulate">The manipulation to perform.</param>
		public void Manipulate(Action<ManipulableBitmap> manipulate)
		{
			manipulate(image);
			NotifyImageChanged();
		}

		/// <summary>
		/// Saves this PreviewBitmap's image.
		/// </summary>
		/// <param name="filename">The file name to save as.</param>
		/// <param name="dim">The Dimensions to save as.</param>
		/// <param name="quality">The quality of the saved JPEG.</param>
		public void Save(Dimensions dim,
			string filename = "imagister.jpg",
			int quality = 100)
		{
			using (Stream result = store.CreateFile("result.jpg"))
			{
				image.SourceImage.SaveJpeg(result,
					dim.Width, dim.Height, 0, quality);
			}
			using (Stream result = store.OpenFile("result.jpg"))
			{
				MediaLibrary lib = new MediaLibrary();
				lib.SavePicture(filename, result);
			}
			store.DeleteFile("result.jpg");
		}
	}
}
