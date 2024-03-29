﻿using System;

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
#if DEBUG
				if (row < 0 || row >= height || col < 0 || col >= width)
					throw new IndexOutOfRangeException();
#endif
				return pixels[row * width + col];
			}
			set
			{
#if DEBUG
				if (row < 0 || row >= height || col < 0 || col >= width)
					throw new IndexOutOfRangeException();
#endif
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
		/// <exception cref="ArgumentException">
		/// If the pixels array's size is not height * width.</exception>
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

	/// <summary>
	/// Represents the size and location of a rectangle.
	/// </summary>
	public struct PixRect
	{
		/// <summary>
		/// The row index of the top-left corner of this Rectangle.
		/// </summary>
		public int Row;
		/// <summary>
		/// The column index of the top-left corner of this Rectangle.
		/// </summary>
		public int Col;
		/// <summary>
		/// The width of this Rectangle.
		/// </summary>
		public int Width;
		/// <summary>
		/// The height of this Rectangle.
		/// </summary>
		public int Height;

		/// <summary>
		/// Constructs a Rectangle.
		/// </summary>
		/// <param name="row">
		/// The row index of the top-left corner of the Rectangle.
		/// </param>
		/// <param name="col">
		/// The column index of the top-left corner of the Rectangle.
		/// </param>
		/// <param name="width">The width of the Rectangle.</param>
		/// <param name="height">The height of the Rectangle.</param>
		public PixRect(int row, int col, int width, int height)
		{
			Row = row;
			Col = col;
			Width = width;
			Height = height;
		}

		/// <summary>
		/// Indicates whether this Rectangle contains a coordinate.
		/// </summary>
		/// <param name="row">The row index of the coordinate.</param>
		/// <param name="col">The column index of the coordinate.</param>
		/// <returns>true if this Rectangle contains the coordinate.</returns>
		public bool Contains(int row, int col)
		{
			return row >= Row && row <= Row + Height &&
				col >= Col && col <= Col + Width;
		}
	}

	/// <summary>
	/// An IPixels that is a selection from another IPixels.
	/// </summary>
	public class Selection : IPixels
	{
		private IPixels pixels;
		private PixRect rect;

		/// <summary>
		/// Gets the height of this Selection.
		/// </summary>
		public int Height
		{
			get { return rect.Height; }
		}

		/// <summary>
		/// Gets the width of this Selection.
		/// </summary>
		public int Width
		{
			get { return rect.Width; }
		}

		/// <summary>
		/// Gets or sets the premultiplied ARGB pixel
		/// at the specified row and column indices in this Selection.
		/// </summary>
		/// <param name="row">The row index of the pixel.</param>
		/// <param name="col">The column index of the pixel.</param>
		public int this[int row, int col]
		{
			get { return pixels[row + rect.Row, col + rect.Col]; }
			set { pixels[row + rect.Row, col + rect.Col] = value; }
		}

		/// <summary>
		/// Constructs a new Selection.
		/// </summary>
		/// <param name="pixels">The IPixels from which
		/// pixels should be selected.</param>
		/// <param name="rect">The Rectangle of pixels to use.</param>
		public Selection(IPixels pixels, PixRect rect)
		{
			this.pixels = pixels;
			this.rect = rect;
		}
	}
}
