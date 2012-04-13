﻿using System;

namespace Imagister
{
	/// <summary>
	/// A Shader is a method that shades a pixel.
	/// </summary>
	/// <param name="pixel">The premultiplied ARGB color of the pixel.</param>
	/// <returns>The premultiplied ARGB color after shading.</returns>
	public delegate int Shader(int pixel);

	/// <summary>
	/// A CoordShader is a method that shades a pixel based on its coordinates.
	/// </summary>
	/// <param name="pixel">The premultiplied ARGB color of the pixel.</param>
	/// <param name="row">The row index of the pixel.</param>
	/// <param name="col">The column index of the pixel.</param>
	/// <returns>The premultiplied ARGB color after shading.</returns>
	public delegate int CoordShader(int pixel, int row, int col);

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
					pixels[row, col] = shader(pixels[row, col]);
				}
			}
		}

		/// <summary>
		/// Applies a Shader to the IPixels.
		/// </summary>
		/// <param name="pixels">The IPixels to shade.</param>
		/// <param name="shader">The CoordShader to apply to the pixels.</param>
		public static void Apply(IPixels pixels, CoordShader shader)
		{
			for (int row = 0; row < pixels.Height; row++)
			{
				for (int col = 0; col < pixels.Width; col++)
				{
					pixels[row, col] = shader(pixels[row, col], row, col);
				}
			}
		}

		/// <summary>
		/// Inverts an ARGB pixel.
		/// </summary>
		/// <param name="pixel">The ARGB pixel to invert.</param>
		/// <returns>The inverted ARGB pixel.</returns>
		public static int Invert(int pixel)
		{
			int a = (pixel >> 24) & 0xFF;
			int r = (pixel >> 16) & 0xFF;
			int g = (pixel >> 8) & 0xFF;
			int b = (pixel) & 0xFF;

			r = 255 - r;
			g = 255 - g;
			b = 255 - b;

			return (a << 24) | (r << 16) | (g << 8) | b;
		}

		/// <summary>
		/// Converts an ARGB pixel to grayscale.
		/// </summary>
		/// <param name="pixel">The ARGB pixel to convert.</param>
		/// <returns>The converted ARGB pixel.</returns>
		public static int Grayscale(int pixel)
		{
			int a = (pixel >> 24) & 0xFF;
			int r = (pixel >> 16) & 0xFF;
			int g = (pixel >> 8) & 0xFF;
			int b = (pixel) & 0xFF;

			int gray = (r + b + g) / 3;

			return (a << 24) | (gray << 16) | (gray << 8) | gray;
		}

		/// <summary>
		/// Converts an ARGB pixel to sepia.
		/// </summary>
		/// <param name="pixel">The ARGB pixel to convert.</param>
		/// <returns>The converted ARGB pixel.</returns>
		public static int Sepia(int pixel)
		{
			int a = (pixel >> 24) & 0xFF;
			int r = (pixel >> 16) & 0xFF;
			int g = (pixel >> 8) & 0xFF;
			int b = (pixel) & 0xFF;

			int outR = Math.Min((int)(.393 * r + .769 * g + .189 * b), 255);
			int outG = Math.Min((int)(.349 * r + .686 * g + .168 * b), 255);
			int outB = Math.Min((int)(.272 * r + .534 * g + .131 * b), 255);

			return (a << 24) | (outR << 16) | (outG << 8) | outB;
		}
	}
}
