using System;

namespace Imagister
{
	public class Posterizer
	{
		public static Vector3 Posterize(Vector3 res, int depth)
		{
			res.Multiply(depth / 256.0f);
			res.Floor();
			res.Multiply(256.0f / depth);
			res.Floor();
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
}
