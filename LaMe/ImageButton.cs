using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaMe
{
	public partial class ImageButton : UserControl
	{
		public Image ImageNormal { get; set; }
		public Image ImageHover { get; set; }
		public Image ImageDown { get; set; }
		public Image ImageDisabled { get; set; }

		private bool hover = false;
		private bool down = false;

		public ImageButton()
		{
			InitializeComponent();

			BackColor = Color.Transparent;
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			var image = ImageNormal;

			if (!Enabled)
			{
				if (ImageDisabled != null)
					image = ImageDisabled;
			}
			else
			{
				if (down)
					image = ImageDown;
				else if (hover)
					image = ImageHover;
			}

			if (image != null)
			{
				if (Width != image.Width || Height != image.Height)
				{
					Width = image.Width;
					Height = image.Height;
				}

				pe.Graphics.DrawImage(image, Point.Empty);
			}
			else
			{
				pe.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));

				var size = pe.Graphics.MeasureString(Name, Font);
				pe.Graphics.DrawString(Name, Font, Brushes.Black, new PointF(Width / 2 - size.Width / 2, Height / 2 - size.Height / 2));
			}
		}

		private void ImageButton_MouseEnter(object sender, EventArgs e)
		{
			hover = true;
			Refresh();
		}

		private void ImageButton_MouseLeave(object sender, EventArgs e)
		{
			hover = false;
			Refresh();
		}

		private void ImageButton_MouseDown(object sender, MouseEventArgs e)
		{
			down = true;
			Refresh();
		}

		private void ImageButton_MouseUp(object sender, MouseEventArgs e)
		{
			down = false;
			Refresh();
		}
	}
}
