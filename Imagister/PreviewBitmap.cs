using System;
using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone;

namespace Imagister
{
	/// <summary>
	/// Represents a preview of a ManipulableBitmap.
	/// </summary>
	public class PreviewBitmap : INotifyPropertyChanged
	{
		private string path;
		private ManipulableBitmap image;

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

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
		/// Gets this PreviewBitmap's JPEG source Stream.
		/// </summary>
		private Stream StreamSource
		{
			get
			{
				using (IsolatedStorageFile dir =
					IsolatedStorageFile.GetUserStoreForApplication())
				{
					return dir.OpenFile(path, FileMode.Open, FileAccess.Read);
				}
			}
		}

		/// <summary>
		/// Constructs a PreviewBitmap.
		/// </summary>
		/// <param name="stream">The JPEG source Stream for the image.</param>
		public PreviewBitmap(Stream stream)
		{
			path = CopyJpegStream(stream);
		}

		/// <summary>
		/// Copies a JPEG Stream into a file.
		/// </summary>
		/// <param name="stream">The JPEG Stream to copy.</param>
		/// <returns>The path to the copied JPEG file
		/// in isolated storage.</returns>
		private static string CopyJpegStream(Stream stream)
		{
			string path = Guid.NewGuid().ToString() + ".jpg";
			using (IsolatedStorageFile dir =
				IsolatedStorageFile.GetUserStoreForApplication())
			{
				using (Stream dest = dir.CreateFile(path))
				{
					stream.CopyTo(dest);
				}
			}
			return path;
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
		/// Manipulates this PreviewBitmap's underlying ManipulableBitmap.
		/// </summary>
		/// <param name="manipulate">The manipulation to perform.</param>
		public void Manipulate(Action<ManipulableBitmap> manipulate)
		{
			manipulate(image);
			NotifyImageChanged();
		}
	}
}
