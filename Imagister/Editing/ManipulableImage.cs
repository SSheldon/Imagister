using System;

namespace Imagister
{
	/// <summary>
	/// Represents an image that can be manipulated.
	/// </summary>
	public class ManipulableImage : IPixels
	{
		private PixelsArray pixels;

		/// <summary>
		/// Gets the height, in pixels, of this ManipulableImage.
		/// </summary>
		public int Height
		{
			get { return pixels.Height; }
		}

		/// <summary>
		/// Gets the width, in pixels, of this ManipulableImage.
		/// </summary>
		public int Width
		{
			get { return pixels.Width; }
		}

		/// <summary>
		/// Gets the pixel array of this ManipulableImage.
		/// </summary>
		public int[] Pixels
		{
			get { return pixels.Pixels; }
		}

		/// <summary>
		/// Gets or sets the premultiplied ARGB pixel
		/// at the specified row and column indices.
		/// </summary>
		/// <param name="row">The row index of the pixel.</param>
		/// <param name="col">The column index of the pixel.</param>
		public int this[int row, int col]
		{
			get
			{
				return pixels[row, col];
			}
			set
			{
				pixels[row, col] = value;
			}
		}

		/// <summary>
		/// Constructs a ManipulableImage.
		/// </summary>
		/// <param name="height">The height of the image.</param>
		/// <param name="width">The width of the image.</param>
		/// <param name="pixels">The ARGB color values for each of
		/// the image's pixels in row-major order.</param>
		public ManipulableImage(int height, int width, int[] pixels)
		{
			this.pixels = new PixelsArray(height, width, pixels);
		}

		/// <summary>
		/// Flips this ManipulableImage vertically.
		/// </summary>
		public void FlipVertical()
		{
			int[] buffer = new int[Width];
			for (int row = 0; row < Height / 2; row++)
			{
				int flippedRow = Height - 1 - row;
				int rowBytes = Width * sizeof(int);
				Buffer.BlockCopy(
					Pixels, row * rowBytes,
					buffer, 0,
					rowBytes);
				Buffer.BlockCopy(
					Pixels, flippedRow * rowBytes,
					Pixels, row * rowBytes,
					rowBytes);
				Buffer.BlockCopy(
					buffer, 0,
					Pixels, flippedRow * rowBytes,
					rowBytes);
			}
		}

		/// <summary>
		/// Flips this ManipulableImage horizontally.
		/// </summary>
		public void FlipHorizontal()
		{
			for (int row = 0; row < Height; row++)
			{
				Array.Reverse(Pixels, row * Width, Width);
			}
		}

		/// <summary>
		/// Rotates this ManipulableImage 180° to be upside-down.
		/// </summary>
		public void RotateDown()
		{
			FlipVertical();
			FlipHorizontal();
		}

		/// <summary>
		/// Rotates this ManipulableImage 90° to the right.
		/// </summary>
		public void RotateRight()
		{
			RotateRight(new int[Height * Width]);
		}

		/// <summary>
		/// Rotates this ManipulableImage 90° to the right.
		/// </summary>
		/// <param name="pixels">The new array that will be used to store
		/// this ManipulableImage's pixels.</param>
		public void RotateRight(int[] pixels)
		{
			PixelsArray old = this.pixels;
			this.pixels = new PixelsArray(old.Width, old.Height, pixels);

			for (int row = 0; row < Height; row++)
			{
				for (int col = 0; col < Width; col++)
				{
					this[row, col] = old[old.Height - 1 - col, row];
				}
			}
		}

		/// <summary>
		/// Rotates this ManipulableImage 90° to the left.
		/// </summary>
		public void RotateLeft()
		{
			RotateLeft(new int[Height * Width]);
		}

		/// <summary>
		/// Rotates this ManipulableImage 90° to the left.
		/// </summary>
		/// <param name="pixels">The new array that will be used to store
		/// this ManipulableImage's pixels.</param>
		public void RotateLeft(int[] pixels)
		{
			PixelsArray old = this.pixels;
			this.pixels = new PixelsArray(old.Width, old.Height, pixels);

			for (int row = 0; row < Height; row++)
			{
				for (int col = 0; col < Width; col++)
				{
					this[row, col] = old[col, old.Width - 1 - row];
				}
			}
		}

		/// <summary>
		/// Crops this ManipulableImage to the specified Rectangle.
		/// </summary>
		/// <param name="rect">The Rectangle to crop to.</param>
		public void Crop(PixRect rect)
		{
			Crop(rect, new int[rect.Height * rect.Width]);
		}

		/// <summary>
		/// Crops this ManipulableImage to the specified Rectangle.
		/// </summary>
		/// <param name="rect">The Rectangle to crop to.</param>
		/// <param name="pixels">The new array that will be used to store
		/// this ManipulableImage's pixels.</param>
		public void Crop(PixRect rect, int[] pixels)
		{
			for (int row = rect.Row; row < rect.Row + rect.Height; row++)
			{
				Buffer.BlockCopy(
					Pixels, sizeof(int) * (row * Width + rect.Col),
					pixels, sizeof(int) * (row - rect.Row) * rect.Width,
					sizeof(int) * rect.Width);
			}
			this.pixels = new PixelsArray(rect.Height, rect.Width, pixels);
		}

		/// <summary>
		/// Resizes this ManipulableImage.
		/// </summary>
		/// <param name="height">The height to which to resize
		/// this ManipulableImage.</param>
		/// <param name="width">The width to which to resize
		/// this ManipulableImage.</param>
		public void Resize(int height, int width)
		{
			Resize(height, width, new int[height * width]);
		}

		/// <summary>
		/// Resizes this ManipulableImage.
		/// </summary>
		/// <param name="height">The height to which to resize
		/// this ManipulableImage.</param>
		/// <param name="width">The width to which to resize
		/// this ManipulableImage.</param>
		/// <param name="pixels">The new array that will be used to store
		/// this ManipulableImage's pixels.</param>
		public void Resize(int height, int width, int[] pixels)
		{
			PixelsArray old = this.pixels;
			this.pixels = new PixelsArray(height, width, pixels);

			for (int row = 0; row < Height; row++)
			{
				for (int col = 0; col < Width; col++)
				{
					this[row, col] = old[
						(int)((double)row / Height * old.Height),
						(int)((double)col / Width * old.Width)];
				}
			}
		}

		/// <summary>
		/// Resizes this ManipulableImage using bilinear filtering.
		/// </summary>
		/// <param name="height">The height to which to resize
		/// this ManipulableImage.</param>
		/// <param name="width">The width to which to resize
		/// this ManipulableImage.</param>
		public void BilinearResize(int height, int width)
		{
			BilinearResize(height, width, new int[height * width]);
		}

		/// <summary>
		/// Resizes this ManipulableImage using bilinear filtering.
		/// </summary>
		/// <param name="height">The height to which to resize
		/// this ManipulableImage.</param>
		/// <param name="width">The width to which to resize
		/// this ManipulableImage.</param>
		/// <param name="pixels">The new array that will be used to store
		/// this ManipulableImage's pixels.</param>
		public void BilinearResize(int height, int width, int[] pixels)
		{
			PixelsArray res = new PixelsArray(height, width, pixels);
			BilinearResizer br = new BilinearResizer(this.pixels, res);
			br.Resize();
			this.pixels = res;
		}

		private class BilinearResizer
		{
			//Adapted from http://tech-algorithm.com/articles/bilinear-image-scaling/
			private PixelsArray old, res;
			private float x_ratio, y_ratio;

			public BilinearResizer(PixelsArray old, PixelsArray res)
			{
				this.old = old;
				this.res = res;
				x_ratio = ((float)(old.Width - 1)) / res.Width;
				y_ratio = ((float)(old.Height - 1)) / res.Height;
			}

			public void Resize()
			{
				int[] pixels = res.Pixels;
				int width = res.Width, height = res.Height;
				for (int i = 0; i < pixels.Length; i++)
					pixels[i] = GetPixel(i / width, i % width);
			}

			public int GetPixel(int i, int j)
			{
				int x = (int)(x_ratio * j);
				int y = (int)(y_ratio * i);
				float x_diff = (x_ratio * j) - x;
				float y_diff = (y_ratio * i) - y;
				int index = (y * old.Width + x);

				Vector3 a, b, c, d;
				a = Vector3.FromARGB(old.Pixels[index]);
				b = Vector3.FromARGB(old.Pixels[index + 1]);
				c = Vector3.FromARGB(old.Pixels[index + old.Width]);
				d = Vector3.FromARGB(old.Pixels[index + old.Width + 1]);

				a.Multiply((1 - x_diff) * (1 - y_diff));
				b.Multiply(x_diff * (1 - y_diff));
				c.Multiply((1 - x_diff) * y_diff);
				d.Multiply(x_diff * y_diff);

				a.Add(b);
				a.Add(c);
				a.Add(d);

				return a.ToARGB();
			}
		}

		/// <summary>
		/// Applies a Shader to this ManipulableImage.
		/// </summary>
		/// <param name="shader">The Shader to apply.</param>
		public void Apply(Shader shader)
		{
			int[] p = Pixels;
			for (int i = 0; i < p.Length; i++)
			{
				p[i] = shader(p[i]);
			}
		}
	}
}
