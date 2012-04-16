using System;

namespace Imagister
{
	/// <summary>
	/// A Shader is a method that shades a pixel.
	/// </summary>
	/// <param name="pixel">The premultiplied ARGB color of the pixel.</param>
	/// <returns>The premultiplied ARGB color after shading.</returns>
	public delegate int Shader(int pixel);

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

		/// <summary>
		/// Adjusts the contrast of a pixel.
		/// </summary>
		/// <param name="argb">The ARGB pixel to adjust.</param>
		/// <param name="scale">The value to scale the contrast by.</param>
		/// <returns>The adjusted ARGB pixel.</returns>
		public static int AdjustContrast(int argb, float scale)
		{
			int a = (argb >> 24) & 0xFF;
			int r = (argb >> 16) & 0xFF;
			int g = (argb >> 8) & 0xFF;
			int b = (argb) & 0xFF;

			Vector3 res = new Vector3(r, g, b);
			res.Multiply(1 / 255.0f);
			res.Subtract(0.5f);
			res.Multiply(scale);
			res.Add(0.5f);
			res.Multiply(255.0f);
			res.Clamp(0, 255);

			r = (int)res.X;
			g = (int)res.Y;
			b = (int)res.Z;
			return (a << 24) | (r << 16) | (g << 8) | b;
		}

		/// <summary>
		/// Gets a shader that adjusts the contrast of a pixel.
		/// </summary>
		/// <param name="scale">The value to scale the contrast by.</param>
		/// <returns>A contrast adjustment Shader.</returns>
		public static Shader AdjustContrast(float scale)
		{
			return (int argb) => AdjustContrast(argb, scale);
		}

		/// <summary>
		/// Adjusts the brightness of a pixel.
		/// </summary>
		/// <param name="argb">The ARGB pixel to adjust.</param>
		/// <param name="bright">The value to add to the brightness.</param>
		/// <returns>The adjusted ARGB pixel.</returns>
		public static int Brighten(int argb, int bright)
		{
			int a = (argb >> 24) & 0xFF;
			int r = (argb >> 16) & 0xFF;
			int g = (argb >> 8) & 0xFF;
			int b = (argb) & 0xFF;

			r += bright;
			g += bright;
			b += bright;

			r = Math.Max(0, Math.Min(r, 255));
			g = Math.Max(0, Math.Min(g, 255));
			b = Math.Max(0, Math.Min(b, 255));

			return (a << 24) | (r << 16) | (g << 8) | b;
		}

		/// <summary>
		/// Gets a Shader that adjust the brightness of a pixel.
		/// </summary>
		/// <param name="bright">The value to add to the brightness.</param>
		/// <returns>A brightening Shader.</returns>
		public static Shader Brighten(int bright)
		{
			return (int argb) => Brighten(argb, bright);
		}

		/// <summary>
		/// Gets a Shader that sets all pixels to the specified color.
		/// </summary>
		/// <param name="rgb">RGB color that pixels should be set to.</param>
		/// <returns>A solid color Shader.</returns>
		public static Shader SolidColor(int rgb)
		{
			rgb = rgb & 0xFFFFFF;
			return (int argb) => argb & (0xFF << 24) | rgb;
		}

		/// <summary>
		/// Corrects a pixel with the specified gamma value.
		/// </summary>
		/// <param name="argb">The ARGB pixel to correct.</param>
		/// <param name="gamma">The gamma value of the pixel.</param>
		/// <returns>The corrected ARGB pixel.</returns>
		public static int CorrectGamma(int argb, float gamma)
		{
			int a = (argb >> 24) & 0xFF;
			int r = (argb >> 16) & 0xFF;
			int g = (argb >> 8) & 0xFF;
			int b = (argb) & 0xFF;

			float correction = 1 / gamma;
			r = (int)(255 * Math.Pow(r / 255.0, correction));
			g = (int)(255 * Math.Pow(g / 255.0, correction));
			b = (int)(255 * Math.Pow(b / 255.0, correction));

			return (a << 24) | (r << 16) | (g << 8) | b;
		}

		/// <summary>
		/// Gets a Shader that corrects pixels for the specified gamma value.
		/// </summary>
		/// <param name="gamma">The gamma value of the pixels.</param>
		/// <returns>A gamma correcting shader.</returns>
		public static Shader CorrectGamma(float gamma)
		{
			return (int argb) => CorrectGamma(argb, gamma);
		}
	}
}
