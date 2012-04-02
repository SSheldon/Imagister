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

		private Stream LoadImageStream(string filename)
		{
			string name = "Imagister.Tests.Images." + filename;
			Assembly asm = Assembly.GetExecutingAssembly();
			return asm.GetManifestResourceStream(name);
		}

		private ManipulableImage LoadManipulableImage(string filename)
		{
			Bitmap bmp = new Bitmap(LoadImageStream(filename));
			return BitmapInterop.Load(bmp);
		}

		/// <summary>
		/// Indicates whether two ManipulableImages have the same data.
		/// </summary>
		private static bool SameData(ManipulableImage a, ManipulableImage b)
		{
			return (a == null) == (b == null) &&
				a.Width == b.Width &&
				a.Height == b.Height &&
				a.Pixels.SequenceEqual<int>(b.Pixels);
		}

		[TestInitialize]
		public void TestInitialize()
		{
			img = LoadManipulableImage("1.bmp");
		}

		[TestMethod]
		public void TestEquals()
		{
			Assert.IsNotNull(img);
			ManipulableImage expected = LoadManipulableImage("1.bmp");
			Assert.IsTrue(SameData(img, expected));
		}
	}
}
