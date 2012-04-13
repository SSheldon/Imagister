using System;

namespace Imagister
{
	/// <summary>
	/// Provides methods for modifying pixel colors.
	/// </summary>
	public static class Colors
	{
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
		public static Shader GetContrastShader(float scale)
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
		public static Shader GetBrightenShader(int bright)
		{
			return (int argb) => Brighten(argb, bright);
		}

		/// <summary>
		/// Gets a Shader that sets all pixels to the specified color.
		/// </summary>
		/// <param name="rgb">RGB color that pixels should be set to.</param>
		/// <returns>A solid color Shader.</returns>
		public static Shader GetSolidShader(int rgb)
		{
			rgb = rgb & 0xFFFFFF;
			return (int argb) => argb & (0xFF << 24) | rgb;
		}
	}
}