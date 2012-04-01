using System.Drawing;

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
			return null;
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
