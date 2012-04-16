using System;
using System.Runtime.InteropServices;

namespace Imagister
{
	/// <summary>
	/// Represents a premultiplied ARGB pixel.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	public struct Pixel
	{
		/// <summary>
		/// The int representation of this Pixel.
		/// </summary>
		[FieldOffset(0)]
		public int ARGB;

		[FieldOffset(0)]
		private byte b;
		[FieldOffset(1)]
		private byte g;
		[FieldOffset(2)]
		private byte r;
		[FieldOffset(3)]
		private byte a;

		/// <summary>The alpha component of this Pixel.</summary>
		public int A
		{
			get { return a; }
		}
		/// <summary>The red component of this Pixel.</summary>
		public int R
		{
			get { return r; }
			set { r = (byte)Math.Max(0, Math.Min(255, value)); }
		}
		/// <summary>The green component of this Pixel.</summary>
		public int G
		{
			get { return g; }
			set { g = (byte)Math.Max(0, Math.Min(255, value)); }
		}
		/// <summary>The blue component of this Pixel.</summary>
		public int B
		{
			get { return b; }
			set { b = (byte)Math.Max(0, Math.Min(255, value)); }
		}

		/// <summary>
		/// Constructs a Pixel.
		/// </summary>
		/// <param name="argb">The argb value for the Pixel.</param>
		public Pixel(int argb)
		{
			a = r = b = g = 0;
			ARGB = argb;
		}

		/// <summary>
		/// Adds another Pixel to this Pixel.
		/// </summary>
		/// <param name="other">The Pixel to be added.</param>
		public void Add(Pixel other)
		{
			R += other.R;
			G += other.G;
			B += other.B;
		}

		/// <summary>
		/// Subtracts another Pixel from this Pixel.
		/// </summary>
		/// <param name="other">The Pixel to be subtracted.</param>
		public void Subtract(Pixel other)
		{
			R -= other.R;
			G -= other.G;
			B -= other.B;
		}
	}

	public struct Vector3
	{
		public float X, Y, Z;

		public Vector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public void Add(Vector3 v)
		{
			X += v.X;
			Y += v.Y;
			Z += v.Z;
		}

		public void Subtract(Vector3 v)
		{
			X -= v.X;
			Y -= v.Y;
			Z -= v.Z;
		}

		public void Add(float c)
		{
			X += c;
			Y += c;
			Z += c;
		}

		public void Subtract(float c)
		{
			X -= c;
			Y -= c;
			Z -= c;
		}

		public void Multiply(float c)
		{
			X *= c;
			Y *= c;
			Z *= c;
		}

		public void Floor()
		{
			X = (float)Math.Floor(X);
			Y = (float)Math.Floor(Y);
			Z = (float)Math.Floor(Z);
		}

		public void Round()
		{
			X = (float)Math.Round(X);
			Y = (float)Math.Round(Y);
			Z = (float)Math.Round(Z);
		}

		public void Clamp(float low, float high)
		{
			X = Math.Max(low, Math.Min(X, high));
			Y = Math.Max(low, Math.Min(Y, high));
			Z = Math.Max(low, Math.Min(Z, high));
		}

		public int ToARGB(int a = 0xFF)
		{
			int r = (int)Math.Max(0, Math.Min(X, 255));
			int g = (int)Math.Max(0, Math.Min(Y, 255));
			int b = (int)Math.Max(0, Math.Min(Z, 255));
			return (a << 24) | (r << 16) | (g << 8) | b;
		}

		public static Vector3 FromARGB(int argb)
		{
			return new Vector3(
				(argb >> 16) & 0xFF,
				(argb >> 8) & 0xFF,
				argb & 0xFF);
		}
	}
}
