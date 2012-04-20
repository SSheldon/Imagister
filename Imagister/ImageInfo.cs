using System;
using System.IO;
using System.Windows;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;

namespace Imagister
{
	public abstract class ImageInfo
	{
		public abstract Stream Stream
		{
			get;
		}

		public virtual ManipulableBitmap Bitmap
		{
			get { return new ManipulableBitmap(Stream); }
		}
	}

	public class ImageUri : ImageInfo
	{
		private Uri uri;

		public ImageUri(Uri uri)
		{
			this.uri = uri;
		}

		public override Stream Stream
		{
			get { return Application.GetResourceStream(uri).Stream; }
		}
	}

	public class ImageResult : ImageInfo
	{
		private PhotoResult result;

		public ImageResult(PhotoResult result)
		{
			this.result = result;
		}

		public override Stream Stream
		{
			get { return result.ChosenPhoto; }
		}
	}

	public class ImageToken : ImageInfo
	{
		private string token;
		private MediaLibrary lib;

		public ImageToken(string token)
		{
			this.token = token;
			this.lib = new MediaLibrary();
		}

		public Picture Picture
		{
			get { return lib.GetPictureFromToken(token); }
		}

		public override Stream Stream
		{
			get { return Picture.GetImage(); }
		}
	}
}
