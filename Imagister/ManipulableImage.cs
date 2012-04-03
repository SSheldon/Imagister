using System;

namespace Imagister
{
	/// <summary>
	/// Represents an image that can be manipulated.
	/// </summary>
	public class ManipulableImage
	{
		private int height, width;
		private int[] pixels;

		/// <summary>
		/// Gets the height, in pixels, of this ManipulableImage.
		/// </summary>
		public int Height
		{
			get { return height; }
		}

		/// <summary>
		/// Gets the width, in pixels, of this ManipulableImage.
		/// </summary>
		public int Width
		{
			get { return width; }
		}

		/// <summary>
		/// Gets the pixel array of this ManipulableImage.
		/// </summary>
		public int[] Pixels
		{
			get { return pixels; }
		}

		/// <summary>
		/// Constructs a ManipulableImage.
		/// </summary>
		/// <param name="height">The height of the image.</param>
		/// <param name="width">The width of the image.</param>
		/// <param name="pixels">The ARGB color values for each of
		/// the image's pixels in row-major order.</param>
		public ManipulableImage(int height, int width, int[] pixels)
		{
			this.height = height;
			this.width = width;
			this.pixels = pixels;
		}

		/// <summary>
		/// Flips this ManipulableImage vertically.
		/// </summary>
		public void FlipVertical()
		{
		}

		/// <summary>
		/// Flips this ManipulableImage horizontally.
		/// </summary>
		public void FlipHorizontal()
		{
		}

		/// <summary>
		/// Rotates this ManipulableImage 180° to be upside-down.
		/// </summary>
		public void RotateDown()
		{
		}

		/// <summary>
		/// Rotates this ManipulableImage 90° to the right.
		/// </summary>
		public void RotateRight()
		{
			RotateRight(new int[height * width]);
		}

		/// <summary>
		/// Rotates this ManipulableImage 90° to the right.
		/// </summary>
		/// <param name="pixels">The new array that will be used to store
		/// this ManipulableImage's pixels.</param>
		public void RotateRight(int[] pixels)
		{
		}

		/// <summary>
		/// Rotates this ManipulableImage 90° to the left.
		/// </summary>
		public void RotateLeft()
		{
			RotateLeft(new int[height * width]);
		}

		/// <summary>
		/// Rotates this ManipulableImage 90° to the left.
		/// </summary>
		/// <param name="pixels">The new array that will be used to store
		/// this ManipulableImage's pixels.</param>
		public void RotateLeft(int[] pixels)
		{
		}

		/// <summary>
		/// Resizes this ManipulableImage.
		/// </summary>
		/// <param name="height">The height to which to resize
		/// this ManipulableImage.</param>
		/// <param name="width">The width to which to resize
		/// this ManipulableImage.</param>
		public void Resize(int height, int width)
		{
			Resize(height, width, new int[height * width]);
		}

		/// <summary>
		/// Resizes this ManipulableImage.
		/// </summary>
		/// <param name="height">The height to which to resize
		/// this ManipulableImage.</param>
		/// <param name="width">The width to which to resize
		/// this ManipulableImage.</param>
		/// <param name="pixels">The new array that will be used to store
		/// this ManipulableImage's pixels.</param>
		public void Resize(int height, int width, int[] pixels)
		{
		}
	}
}
