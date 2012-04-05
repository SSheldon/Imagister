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
}
