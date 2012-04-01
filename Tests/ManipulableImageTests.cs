using System.IO;
using System.Drawing;
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
			Assert.AreEqual<ManipulableImage>(expected, img);
		}
	}
}
