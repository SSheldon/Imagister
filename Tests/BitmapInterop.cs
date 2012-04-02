using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Imagister
{
	/// <summary>
	/// Provides methods for interoperability between
	/// ManipulableImage and Bitmap.
	/// </summary>
	public static class BitmapInterop
	{
		/// <summary>
		/// Loads a Bitmap into a ManipulableImage.
		/// </summary>
		/// <param name="bmp">The Bitmap to load.</param>
		/// <returns>The created ManipulableImage.</returns>
		public static ManipulableImage Load(Bitmap bmp)
		{
			int[] pixels = new int[bmp.Width * bmp.Height];
			BitmapData data = bmp.LockBits(
				new Rectangle(0, 0, bmp.Width, bmp.Height),
				ImageLockMode.ReadOnly,
				PixelFormat.Format32bppPArgb);
			Marshal.Copy(data.Scan0, pixels, 0, pixels.Length);
			bmp.UnlockBits(data);
			return new ManipulableImage(bmp.Height, bmp.Width, pixels);
		}

		/// <summary>
		/// Saves a ManipulableImage to a Bitmap.
		/// </summary>
		/// <param name="img">The ManipulableImage to save.</param>
		/// <returns>The created Bitmap.</returns>
		public static Bitmap Save(ManipulableImage img)
		{
			return null;
		}
	}
}
