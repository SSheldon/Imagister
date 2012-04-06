using System;

namespace Imagister
{
	/// <summary>
	/// Represents a rectangle of pixels.
	/// </summary>
	public interface IPixels
	{
		/// <summary>
		/// Gets the height of this IPixels.
		/// </summary>
		int Height
		{
			get;
		}

		/// <summary>
		/// Gets the width of this IPixels.
		/// </summary>
		int Width
		{
			get;
		}

		/// <summary>
		/// Gets or sets the premultiplied ARGB pixel
		/// at the specified row and column indices in this IPixels.
		/// </summary>
		/// <param name="row">The row index of the pixel.</param>
		/// <param name="col">The column index of the pixel.</param>
		int this[int row, int col]
		{
			get;
			set;
		}
	}

	/// <summary>
	/// An IPixels with the pixels stored in an int array.
	/// </summary>
	public class PixelsArray : IPixels
	{
		private readonly int height, width;
		private readonly int[] pixels;

		/// <summary>
		/// Gets the height of this PixelsArray.
		/// </summary>
		public int Height
		{
			get { return height; }
		}

		/// <summary>
		/// Gets the width of this PixelsArray.
		/// </summary>
		public int Width
		{
			get { return width; }
		}

		/// <summary>
		/// Gets or sets the premultiplied ARGB pixel at
		/// the specified row and column indices in this PixelsArray.
		/// </summary>
		/// <param name="row">The row index of the pixel.</param>
		/// <param name="col">The column index of the pixel.</param>
		public int this[int row, int col]
		{
			get
			{
				if (row < 0 || row >= height || col < 0 || col >= width)
					throw new IndexOutOfRangeException();
				return pixels[row * width + col];
			}
			set
			{
				if (row < 0 || row >= height || col < 0 || col >= width)
					throw new IndexOutOfRangeException();
				pixels[row * width + col] = value;
			}
		}

		/// <summary>
		/// Gets the pixel array of this PixelsArray.
		/// </summary>
		public int[] Pixels
		{
			get { return pixels; }
		}

		/// <summary>
		/// Constructs a PixelsArray.
		/// </summary>
		/// <param name="height">The height of the PixelsArray.</param>
		/// <param name="width">The width of the PixelsArray.</param>
		/// <param name="pixels">The array that will be used to store
		/// the pixels of the PixelArray.</param>
		public PixelsArray(int height, int width, int[] pixels)
		{
			if (pixels.Length != height * width)
				throw new ArgumentException();
			this.height = height;
			this.width = width;
			this.pixels = pixels;
		}

		/// <summary>
		/// Constructs a PixelsArray of cleared pixels.
		/// </summary>
		/// <param name="height">The height of the PixelsArray.</param>
		/// <param name="width">The width of the PixelsArray.</param>
		public PixelsArray(int height, int width)
			: this(height, width, new int[height * width])
		{ }
	}
}
