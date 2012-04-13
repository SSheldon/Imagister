using System;

namespace Imagister
{
	/// <summary>
	/// Provides methods for drawing shapes on IPixels.
	/// </summary>
	public static class Drawing
	{
		/// <summary>
		/// Draws a filled rectangle.
		/// </summary>
		/// <param name="pixels">The IPixels to draw on.</param>
		/// <param name="rect">The rectangle to draw.</param>
		/// <param name="rgb">The color to use.</param>
		public static void FillRect(IPixels pixels, PixRect rect, int rgb)
		{
			Selection selection = new Selection(pixels, rect);
			Shaders.Apply(pixels, Colors.GetSolidShader(rgb));
		}
	}
}
