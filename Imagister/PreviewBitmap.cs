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
		/// Indicates whether this PreviewBitmap is loaded.
		/// </summary>
		public bool IsLoaded
		{
			get { return path != null && image != null; }
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

		#region Isolated Storage IO
		/// <summary>
		/// Gets the user-scoped isolated storage for Imagister.
		/// </summary>
		public static IsolatedStorageFile ImagisterStore
		{
			get { return IsolatedStorageFile.GetUserStoreForApplication(); }
		}

		/// <summary>
		/// Clears the isolated storage for Imagister.
		/// </summary>
		public static void ClearImagisterStore()
		{
			using (IsolatedStorageFile dir = ImagisterStore)
			{
				dir.Remove();
			}
		}

		/// <summary>
		/// Gets this PreviewBitmap's JPEG source Stream.
		/// </summary>
		private Stream StreamSource
		{
			get
			{
				using (IsolatedStorageFile dir = ImagisterStore)
				{
					return dir.OpenFile(path, FileMode.Open, FileAccess.Read);
				}
			}
		}

		/// <summary>
		/// Creates this PreviewBitmap's source JPEG.
		/// </summary>
		/// <param name="path">The path of the JPEG.</param>
		/// <returns>The created JPEG's stream.</returns>
		private IsolatedStorageFileStream CreateSource(string path)
		{
			this.path = path;
			IsolatedStorageFileStream stream;
			using (IsolatedStorageFile dir = ImagisterStore)
			{
				stream = dir.CreateFile(path);
			}
			return stream;
		}

		/// <summary>
		/// Creates this PreviewBitmap's source JPEG.
		/// </summary>
		/// <returns>The created JPEG's stream.</returns>
		private IsolatedStorageFileStream CreateSource()
		{
			string path = Guid.NewGuid().ToString() + ".jpg";
			return CreateSource(path);
		}

		/// <summary>
		/// Deletes this PreviewBitmap's source JPEG.
		/// </summary>
		private void DeleteSource()
		{
			using (IsolatedStorageFile dir = ImagisterStore)
			{
				dir.DeleteFile(path);
			}
			path = null;
		}
		#endregion

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
