using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imagister.Tests
{
	[TestClass]
	public class PixelImageTests
	{
		private ManipulableImage img;

		/// <summary>
		/// Does an action and then compares this Test's image
		/// with the image with the specified filename.
		/// </summary>
		/// <param name="action">The Action to do.</param>
		/// <param name="filename">The name of the image
		/// to compare to after the action is done.</param>
		private void DoAndCompare(Action action, string filename)
		{
			action();
			ManipulableImage expected =
				ManipulableImageTests.LoadManipulableImage(filename);
			Assert.IsTrue(ManipulableImageTests.SameData(img, expected));
		}

		[TestInitialize]
		public void TestInitialize()
		{
			img = ManipulableImageTests.LoadManipulableImage("4.bmp");
		}

		[TestMethod]
		public void TestFlipVertical()
		{
			DoAndCompare(() => img.FlipVertical(), "4_flip-v.bmp");
		}

		[TestMethod]
		public void TestFlipHorizontal()
		{
			DoAndCompare(() => img.FlipHorizontal(), "4_flip-h.bmp");
		}

		[TestMethod]
		public void TestRotateDown()
		{
			DoAndCompare(() => img.RotateDown(), "4_rot-d.bmp");
		}

		[TestMethod]
		public void TestRotateRight()
		{
			DoAndCompare(() => img.RotateRight(), "4_rot-r.bmp");
		}

		[TestMethod]
		public void TestRotateLeft()
		{
			DoAndCompare(() => img.RotateLeft(), "4_rot-l.bmp");
		}

		[TestMethod]
		public void TestInvert()
		{
			DoAndCompare(() => img.Apply(Shaders.Invert), "4_invert.bmp");
		}

		[TestMethod]
		public void TestGrayscale()
		{
			DoAndCompare(() => img.Apply(Shaders.Grayscale), "4_grayscale.bmp");
		}

		[TestMethod]
		public void TestSepia()
		{
			DoAndCompare(() => img.Apply(Shaders.Sepia), "4_sepia.bmp");
		}

		[TestMethod]
		public void TestGamma()
		{
			DoAndCompare(() => img.Apply(Shaders.CorrectGamma(2.0f)),
				"4_gamma-2.bmp");
			TestInitialize();
			DoAndCompare(() => img.Apply(Shaders.CorrectGamma(0.5f)),
				"4_gamma-0.5.bmp");
		}

		[TestMethod]
		public void TestBrighten()
		{
			DoAndCompare(() => img.Apply(Shaders.Brighten(64)),
				"4_brighten-64.bmp");
			TestInitialize();
			DoAndCompare(() => img.Apply(Shaders.Brighten(-64)),
				"4_darken-64.bmp");
		}
	}
}
