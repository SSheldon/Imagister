using System;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Phone;

namespace Imagister
{
	public class ManipulableBitmap : ManipulableImage
	{
		private Stream source;
		private WriteableBitmap bmp;

		/// <summary>
		/// Constructs a ManipulableBitmap.
		/// </summary>
		/// <param name="source">The JPEG source Stream for the image.</param>
		/// <param name="bmp">The WriteableBitmap for the image.</param>
		private ManipulableBitmap(Stream source, WriteableBitmap bmp)
			: base(bmp.PixelHeight, bmp.PixelWidth, bmp.Pixels)
		{
			this.source = source;
			this.bmp = bmp;
		}

		/// <summary>
		/// Constructs a ManipulableBitmap.
		/// </summary>
		/// <param name="source">The JPEG source Stream for the image.</param>
		public ManipulableBitmap(Stream source)
			: this(source, PictureDecoder.DecodeJpeg(source))
		{ }
	}
}
