using System;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Imagister;

namespace Imagister.Tests
{
	[TestClass]
	public class ManipulableImageTests
	{
		private ManipulableImage img;

		/// <summary>
		/// Gets the stream for an embedded test image.
		/// </summary>
		/// <param name="filename">The filename of the image.</param>
		/// <returns>The stream for the image.</returns>
		public static Stream LoadImageStream(string filename)
		{
			string name = "Imagister.Tests.Images." + filename;
			Assembly asm = Assembly.GetExecutingAssembly();
			return asm.GetManifestResourceStream(name);
		}

		/// <summary>
		/// Creates a ManipulableImage from an embedded test image.
		/// </summary>
		/// <param name="filename">The filename of the image.</param>
		/// <returns>The created ManipulableImage</returns>
		public static ManipulableImage LoadManipulableImage(string filename)
		{
			Bitmap bmp = new Bitmap(LoadImageStream(filename));
			return BitmapInterop.Load(bmp);
		}

		/// <summary>
		/// Indicates whether two ManipulableImages have the same data.
		/// </summary>
		public static bool SameData(ManipulableImage a, ManipulableImage b)
		{
			return (a == null) == (b == null) &&
				a.Width == b.Width &&
				a.Height == b.Height &&
				a.Pixels.SequenceEqual<int>(b.Pixels);
		}

		[TestInitialize]
		public void TestInitialize()
		{
			img = LoadManipulableImage("3.bmp");
		}

		[TestMethod]
		public void TestEquals()
		{
			Assert.IsNotNull(img);
			ManipulableImage expected = LoadManipulableImage("3.bmp");
			Assert.IsTrue(SameData(img, expected));
		}

		/// <summary>
		/// Does an action and then compares this Test's image
		/// with the image with the specified filename.
		/// </summary>
		/// <param name="action">The Action to do.</param>
		/// <param name="filename">The name of the image
		/// to compare to after the action is done.</param>
		private void DoAndCompare(Action action, string filename)
		{
			action.Invoke();
			ManipulableImage expected = LoadManipulableImage(filename);
			Assert.IsTrue(SameData(img, expected));
		}

		[TestMethod]
		public void TestFlipVertical()
		{
			DoAndCompare(() => img.FlipVertical(), "3_flip-v.bmp");
		}

		[TestMethod]
		public void TestFlipHorizontal()
		{
			DoAndCompare(() => img.FlipHorizontal(), "3_flip-h.bmp");
		}

		[TestMethod]
		public void TestRotateDown()
		{
			DoAndCompare(() => img.RotateDown(), "3_rot-d.bmp");
		}

		[TestMethod]
		public void TestRotateRight()
		{
			DoAndCompare(() => img.RotateRight(), "3_rot-r.bmp");
		}

		[TestMethod]
		public void TestRotateLeft()
		{
			DoAndCompare(() => img.RotateLeft(), "3_rot-l.bmp");
		}

		[TestMethod]
		[ExpectedException(typeof(System.ArgumentException))]
		public void TestRotateRightInvalidPixels()
		{
			img.RotateRight(new int[3]);
		}
	}
}
