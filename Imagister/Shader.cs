using System;

namespace Imagister
{
	/// <summary>
	/// A Shader is a method that shades a pixel.
	/// </summary>
	/// <param name="row">The row index of the pixel.</param>
	/// <param name="col">The column index of the pixel.</param>
	/// <param name="pixel">The premultiplied ARGB color of the pixel.</param>
	/// <returns>The premultiplied ARGB color after shading.</returns>
	public delegate int Shader(int row, int col, int pixel);

	/// <summary>
	/// Provides Shaders and methods for working with them.
	/// </summary>
	public static class Shaders
	{
		/// <summary>
		/// Applies a Shader to the IPixels.
		/// </summary>
		/// <param name="pixels">The IPixels to shade.</param>
		/// <param name="shader">The Shader to apply to the pixels.</param>
		public static void Apply(IPixels pixels, Shader shader)
		{
			for (int row = 0; row < pixels.Height; row++)
			{
				for (int col = 0; col < pixels.Width; col++)
				{
					pixels[row, col] = shader(row, col, pixels[row, col]);
				}
			}
		}
	}
}
