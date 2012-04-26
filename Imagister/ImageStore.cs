using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace Imagister
{
	/// <summary>
	/// An ImageStore is a store in isolated storage for an image.
	/// </summary>
	public class ImageStore
	{
		/// <summary>
		/// Gets the user-scoped isolated storage for Imagister.
		/// </summary>
		public static IsolatedStorageFile ImagisterStore
		{
			get { return IsolatedStorageFile.GetUserStoreForApplication(); }
		}

		/// <summary>
		/// Clears the isolated storage for Imagister.
		/// </summary>
		public static void ClearImagisterStore()
		{
			using (IsolatedStorageFile dir = ImagisterStore)
			{
				dir.Remove();
			}
		}

		private string id;

		/// <summary>
		/// Constructs an ImageStore.
		/// </summary>
		/// <param name="id">The id for the ImageStore.</param>
		public ImageStore(string id)
		{
			this.id = id;
		}

		/// <summary>
		/// Constructs an ImageStore.
		/// </summary>
		public ImageStore()
			: this(Guid.NewGuid().ToString())
		{
			using (IsolatedStorageFile store = ImagisterStore)
			{
				store.CreateDirectory(id);
			}
		}

		/// <summary>
		/// Gets the path to the file in this ImageStore.
		/// </summary>
		/// <param name="filename">The name of the file.</param>
		/// <returns>The path to the file.</returns>
		private string GetPath(string filename)
		{
			return Path.Combine(id, filename);
		}

		/// <summary>
		/// Creates a file in this ImageStore.
		/// </summary>
		/// <param name="filename">The name of the file.</param>
		/// <returns>The created file's Stream.</returns>
		public IsolatedStorageFileStream CreateFile(string filename)
		{
			using (IsolatedStorageFile store = ImagisterStore)
			{
				string temp = GetPath(filename);
				return store.CreateFile(GetPath(filename));
			}
		}

		/// <summary>
		/// Opens a file in this ImageStore.
		/// </summary>
		/// <param name="filename">The name of the file.</param>
		/// <returns>The file's stream.</returns>
		public IsolatedStorageFileStream OpenFile(string filename)
		{
			using (IsolatedStorageFile store = ImagisterStore)
			{
				return store.OpenFile(GetPath(filename),
					FileMode.Open, FileAccess.Read);
			}
		}

		/// <summary>
		/// Deletes a file in this ImageStore.
		/// </summary>
		/// <param name="filename">The name of the file.</param>
		public void DeleteFile(string filename)
		{
			using (IsolatedStorageFile store = ImagisterStore)
			{
				store.DeleteFile(GetPath(filename));
			}
		}

		/// <summary>
		/// Deletes this ImageStore.
		/// </summary>
		public void Delete()
		{
			using (IsolatedStorageFile store = ImagisterStore)
			{
				string match = id + Path.DirectorySeparatorChar + "*";
				foreach (string file in store.GetFileNames(match))
				{
					store.DeleteFile(GetPath(file));
				}
				store.DeleteDirectory(id);
			}
			id = null;
		}
	}
}
