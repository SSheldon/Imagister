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

		public Vector3(Pixel pixel)
			: this(pixel.R, pixel.G, pixel.B)
		{ }

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

		public void Multiply(float c)
		{
			X *= c;
			Y *= c;
			Z *= c;
		}

		public void Round()
		{
			X = (float)Math.Round(X);
			Y = (float)Math.Round(Y);
			Z = (float)Math.Round(Z);
		}
	}
}
