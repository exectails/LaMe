namespace LaMe
{
	partial class ImageButton
	{
		/// <summary> 
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Komponenten-Designer generierter Code

		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// ImageButton
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "ImageButton";
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageButton_MouseDown);
			this.MouseEnter += new System.EventHandler(this.ImageButton_MouseEnter);
			this.MouseLeave += new System.EventHandler(this.ImageButton_MouseLeave);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImageButton_MouseUp);
			this.ResumeLayout(false);

		}

		#endregion
	}
}
