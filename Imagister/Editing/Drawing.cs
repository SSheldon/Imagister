using System;

namespace Imagister
{
	/// <summary>
	/// Provides methods for drawing shapes on IPixels.
	/// </summary>
	public static class Drawing
	{
		/// <summary>
		/// Draws a line on the IPixels between two points.
		/// </summary>
		/// <param name="pixels">The IPixels to draw on.</param>
		/// <param name="rgb">The color of the line.</param>
		public static void DrawLine(IPixels pixels,
			int x0, int y0, int x1, int y1, int rgb)
		{
			rgb = rgb & 0xFFFFFF;
			//Bresenham line algorithm implementation
			int sx = (x0 < x1 ? 1 : -1), sy = (y0 < y1 ? 1 : -1);
			int dx = Math.Abs(x1 - x0), dy = Math.Abs(y1 - y0);
			int err = dx - dy;
			while (true)
			{
				//set pixel
				pixels[y0, x0] = pixels[y0, x0] & (0xFF << 24) | rgb;
				//check for break
				if (x0 == x1 && y0 == y1) break;
				//advance x0 and y0
				int err2 = 2 * err;
				if (err2 > -dy)
				{
					err -= dy;
					x0 += sx;
				}
				if (err2 < dx)
				{
					err += dx;
					y0 += sy;
				}
			}
		}

		/// <summary>
		/// Draws a filled rectangle.
		/// </summary>
		/// <param name="pixels">The IPixels to draw on.</param>
		/// <param name="rect">The rectangle to draw.</param>
		/// <param name="rgb">The color to use.</param>
		public static void FillRect(IPixels pixels, PixRect rect, int rgb)
		{
			Selection selection = new Selection(pixels, rect);
			Shaders.Apply(selection, Shaders.SolidColor(rgb));
		}
	}
}
