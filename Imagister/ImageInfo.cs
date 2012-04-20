using System;
using System.IO;
using System.Windows;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;

namespace Imagister
{
	/// <summary>
	/// Represents information required to load an image.
	/// </summary>
	public abstract class ImageInfo
	{
		/// <summary>
		/// Gets the JPEG source Stream of the image.
		/// </summary>
		public abstract Stream Stream
		{
			get;
		}

		/// <summary>
		/// Gets a ManipulableBitmap of the image.
		/// </summary>
		public virtual ManipulableBitmap Bitmap
		{
			get { return new ManipulableBitmap(Stream); }
		}
	}

	/// <summary>
	/// Represents information to load an image from a
	/// Uniform Resource Identifier (URI).
	/// </summary>
	public class ImageUri : ImageInfo
	{
		private Uri uri;

		/// <summary>
		/// Constructs an ImageUri.
		/// </summary>
		/// <param name="uri">The URI of the image.</param>
		public ImageUri(Uri uri)
		{
			this.uri = uri;
		}

		/// <summary>
		/// Gets the JPEG source Stream of the image.
		/// </summary>
		public override Stream Stream
		{
			get { return Application.GetResourceStream(uri).Stream; }
		}
	}

	/// <summary>
	/// Represents information to load an image from a PhotoResult.
	/// </summary>
	public class ImageResult : ImageInfo
	{
		private PhotoResult result;

		/// <summary>
		/// Constructs an ImageResult.
		/// </summary>
		/// <param name="result">
		/// The PhotoResult from a PhotoChooserTask.</param>
		public ImageResult(PhotoResult result)
		{
			this.result = result;
		}

		/// <summary>
		/// Gets the JPEG source Stream of the image.
		/// </summary>
		public override Stream Stream
		{
			get { return result.ChosenPhoto; }
		}
	}

	/// <summary>
	/// Represents information to load an image from its picture token.
	/// </summary>
	public class ImageToken : ImageInfo
	{
		private string token;
		private MediaLibrary lib;

		/// <summary>
		/// Constructs an ImageToken.
		/// </summary>
		/// <param name="token">The picture token of the image.</param>
		public ImageToken(string token)
		{
			this.token = token;
			this.lib = new MediaLibrary();
		}

		/// <summary>
		/// Gets the Picture of the image.
		/// </summary>
		public Picture Picture
		{
			get { return lib.GetPictureFromToken(token); }
		}

		/// <summary>
		/// Gets the JPEG source Stream of the image.
		/// </summary>
		public override Stream Stream
		{
			get { return Picture.GetImage(); }
		}
	}
}
