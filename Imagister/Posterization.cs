using System;

namespace Imagister
{
	public class Posterizer
	{
		private int depth;

		public Posterizer(int depth)
		{
			this.depth = depth;
		}

		public int Posterize(int argb)
		{
			return Posterize(argb, depth);
		}

		public static Vector3 Posterize(Vector3 res, int depth)
		{
			res.Multiply(depth / 256.0f);
			res.Round();
			res.Clamp(0, depth - 1);
			res.Multiply(256.0f / depth);
			res.Round();
			res.Clamp(0, 255);
			return res;
		}

		public static int Posterize(int argb, int depth)
		{
			int a = (argb >> 24) & 0xFF;
			int r = (argb >> 16) & 0xFF;
			int g = (argb >> 8) & 0xFF;
			int b = (argb) & 0xFF;

			Vector3 res = new Vector3(r, g, b);
			res = Posterize(res, depth);

			r = (int)res.X;
			g = (int)res.Y;
			b = (int)res.Z;

			return (a << 24) | (r << 16) | (g << 8) | b;
		}
	}

	public class Ditherer
	{
		private Vector3[,] errs;
		private int depth;

		public Ditherer(int depth, int width, int height)
		{
			this.depth = depth;
			this.errs = new Vector3[height, width];
		}

		public Ditherer(int depth, IPixels pixels)
			: this(depth, pixels.Width, pixels.Height)
		{ }

		private void AddErrors(Vector3 diff, int row, int col)
		{
			bool lastCol = col + 1 >= errs.GetLength(1);
			if (!lastCol)
			{
				Vector3 d = diff;
				d.Multiply(0.4375f);
				errs[row, col + 1].Add(d);
			}
			bool lastRow = row + 1 >= errs.GetLength(0);
			if (!lastRow && col != 0)
			{
				Vector3 d = diff;
				d.Multiply(0.1875f);
				errs[row + 1, col - 1].Add(d);
			}
			if (!lastRow)
			{
				Vector3 d = diff;
				d.Multiply(0.3125f);
				errs[row + 1, col].Add(d);
			}
			if (!lastRow && !lastCol)
			{
				Vector3 d = diff;
				d.Multiply(0.0625f);
				errs[row + 1, col + 1].Add(d);
			}
		}

		public int Dither(int argb, int row, int col)
		{
			int a = (argb >> 24) & 0xFF;
			int r = (argb >> 16) & 0xFF;
			int g = (argb >> 8) & 0xFF;
			int b = (argb) & 0xFF;

			Vector3 res = new Vector3(r, g, b);
			res.Add(errs[row, col]);
			res = Posterizer.Posterize(res, depth);

			Vector3 diff = new Vector3(r, g, b);
			diff.Subtract(res);
			AddErrors(diff, row, col);

			r = (int)res.X;
			g = (int)res.Y;
			b = (int)res.Z;
			return (a << 24) | (r << 16) | (g << 8) | b;
		}

		public void Dither(IPixels pixels)
		{
			for (int row = 0; row < pixels.Height; row++)
			{
				for (int col = 0; col < pixels.Width; col++)
				{
					pixels[row, col] = Dither(pixels[row, col], row, col);
				}
			}
		}

		public static void Dither(IPixels pixels, int depth)
		{
			Ditherer ditherer = new Ditherer(depth, pixels);
			ditherer.Dither(pixels);
		}
	}
}
