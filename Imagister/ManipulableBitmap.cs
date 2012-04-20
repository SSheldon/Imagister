using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone;

namespace Imagister
{
	/// <summary>
	/// Represents a Bitmap that can be manipulated.
	/// </summary>
	public class ManipulableBitmap : ManipulableImage
	{
		private WriteableBitmap bmp;

		/// <summary>
		/// Gets an ImageSource for this ManipulableBitmap.
		/// </summary>
		public ImageSource SourceImage
		{
			get { return bmp; }
		}

		/// <summary>
		/// Constructs a ManipulableBitmap.
		/// </summary>
		/// <param name="source">The JPEG source Stream for the image.</param>
		/// <param name="bmp">The WriteableBitmap for the image.</param>
		private ManipulableBitmap(WriteableBitmap bmp)
			: base(bmp.PixelHeight, bmp.PixelWidth, bmp.Pixels)
		{
			this.bmp = bmp;
		}

		/// <summary>
		/// Constructs a ManipulableBitmap.
		/// </summary>
		/// <param name="source">The JPEG source Stream for the image.</param>
		public ManipulableBitmap(Stream source)
			: this(PictureDecoder.DecodeJpeg(source))
		{ }

		/// <summary>
		/// Rotates this ManipulableBitmap 90° to the right.
		/// </summary>
		public override void RotateRight()
		{
			bmp = new WriteableBitmap(bmp.PixelHeight, bmp.PixelWidth);
			RotateRight(bmp.Pixels);
		}

		/// <summary>
		/// Rotates this ManipulableBitmap 90° to the left.
		/// </summary>
		public override void RotateLeft()
		{
			bmp = new WriteableBitmap(bmp.PixelHeight, bmp.PixelWidth);
			RotateLeft(bmp.Pixels);
		}
	}
}
