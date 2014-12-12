using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaMe
{
	public class ImageButton : Button
	{
		public Image ImageNormal { get; set; }
		public Image ImageHover { get; set; }
		public Image ImageDown { get; set; }
		public Image ImageDisabled { get; set; }

		private bool hover = false;
		private bool down = false;

		public ImageButton()
		{
			FlatStyle = FlatStyle.Flat;
			FlatAppearance.BorderSize = 0;
			FlatAppearance.MouseDownBackColor = Color.Transparent;
			FlatAppearance.MouseOverBackColor = Color.Transparent;
			BackColor = Color.Transparent;
			Margin = new Padding(0);
			Text = "";
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

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
				pe.Graphics.DrawImage(image, Point.Empty);
			else
			{
				pe.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
				pe.Graphics.DrawString("ImageButton", Font, Brushes.Black, PointF.Empty);
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);

			hover = true;
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			hover = false;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			down = true;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			down = false;

			this.Refresh();
		}
	}
}
