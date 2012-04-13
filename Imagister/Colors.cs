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
	}
}