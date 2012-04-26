using System;
using System.ComponentModel;

namespace Imagister
{
	/// <summary>
	/// Represents the dimensions of an image.
	/// </summary>
	public class Dimensions : INotifyPropertyChanged
	{
		private readonly int origWidth, origHeight;
		private double scale;

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Gets or sets the width of the image.
		/// </summary>
		public int Width
		{
			get { return (int)(scale * origWidth); }
			set
			{
				Scale = value / (double)origWidth;
			}
		}

		/// <summary>
		/// Gets or sets the height of the image.
		/// </summary>
		public int Height
		{
			get { return (int)(scale * origHeight); }
			set
			{
				Scale = value / (double)origHeight;
			}
		}

		/// <summary>
		/// Gets or sets the maximum dimension of the image.
		/// </summary>
		public int Max
		{
			get { return Math.Max(Width, Height); }
			set
			{
				Scale = Math.Min(1.0, Math.Min(
					value / (double)origWidth,
					value / (double)origHeight));
			}
		}

		/// <summary>
		/// Gets or sets the value the image is scaled by.
		/// </summary>
		public double Scale
		{
			get { return scale; }
			set
			{
				scale = value;
				NotifyChanged();
			}
		}

		/// <summary>
		/// Constructs a Dimensions for an image.
		/// </summary>
		/// <param name="width">The width of the image.</param>
		/// <param name="height">The height of the image.</param>
		public Dimensions(int width, int height)
		{
			origWidth = width;
			origHeight = height;
			scale = 1.0;
		}

		/// <summary>
		/// Raises the PropertyChanged event to notify listeners that
		/// this Dimensions has changed.
		/// </summary>
		private void NotifyChanged()
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(null));
			}
		}
	}
}
