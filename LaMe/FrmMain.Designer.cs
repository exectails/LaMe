namespace LaMe
{
	partial class FrmMain
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

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
			this.WebPatchNotes = new System.Windows.Forms.WebBrowser();
			this.LblStatus = new System.Windows.Forms.Label();
			this.BtnStart = new LaMe.ImageButton();
			this.BtnClose = new LaMe.ImageButton();
			this.SuspendLayout();
			// 
			// WebPatchNotes
			// 
			this.WebPatchNotes.Location = new System.Drawing.Point(21, 58);
			this.WebPatchNotes.MinimumSize = new System.Drawing.Size(20, 20);
			this.WebPatchNotes.Name = "WebPatchNotes";
			this.WebPatchNotes.Size = new System.Drawing.Size(658, 348);
			this.WebPatchNotes.TabIndex = 1;
			this.WebPatchNotes.Url = new System.Uri("", System.UriKind.Relative);
			// 
			// LblStatus
			// 
			this.LblStatus.AutoSize = true;
			this.LblStatus.Location = new System.Drawing.Point(20, 445);
			this.LblStatus.Name = "LblStatus";
			this.LblStatus.Size = new System.Drawing.Size(41, 13);
			this.LblStatus.TabIndex = 3;
			this.LblStatus.Text = "Ready.";
			// 
			// BtnStart
			// 
			this.BtnStart.BackColor = System.Drawing.Color.Transparent;
			this.BtnStart.ImageDisabled = null;
			this.BtnStart.ImageDown = null;
			this.BtnStart.ImageHover = null;
			this.BtnStart.ImageNormal = null;
			this.BtnStart.Location = new System.Drawing.Point(529, 427);
			this.BtnStart.Name = "BtnStart";
			this.BtnStart.Size = new System.Drawing.Size(150, 49);
			this.BtnStart.TabIndex = 5;
			this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
			// 
			// BtnClose
			// 
			this.BtnClose.BackColor = System.Drawing.Color.Transparent;
			this.BtnClose.ImageDisabled = null;
			this.BtnClose.ImageDown = null;
			this.BtnClose.ImageHover = null;
			this.BtnClose.ImageNormal = null;
			this.BtnClose.Location = new System.Drawing.Point(655, 12);
			this.BtnClose.Name = "BtnClose";
			this.BtnClose.Size = new System.Drawing.Size(24, 24);
			this.BtnClose.TabIndex = 4;
			this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(700, 500);
			this.Controls.Add(this.BtnStart);
			this.Controls.Add(this.BtnClose);
			this.Controls.Add(this.LblStatus);
			this.Controls.Add(this.WebPatchNotes);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "LaMe";
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.Load += new System.EventHandler(this.FrmMain_Load);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMain_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmMain_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmMain_MouseUp);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.WebBrowser WebPatchNotes;
		private System.Windows.Forms.Label LblStatus;
		private ImageButton BtnClose;
		private ImageButton BtnStart;
	}
}

