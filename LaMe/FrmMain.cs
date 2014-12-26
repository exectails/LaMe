using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaMe
{
	public partial class FrmMain : Form
	{
		private const string IniFile = "LaMe.ini";
		private const string PackageFolder = "package/";

		private string exeName, exeParameters;

		private bool dragging;
		private int dragStartX, dragStartY;

		public FrmMain()
		{
			InitializeComponent();
		}

		private void FrmMain_MouseDown(object sender, MouseEventArgs e)
		{
			dragging = true;
			dragStartX = e.X;
			dragStartY = e.Y;
		}

		private void FrmMain_MouseMove(object sender, MouseEventArgs e)
		{
			if (dragging)
			{
				this.Left += e.X - dragStartX;
				this.Top += e.Y - dragStartY;
			}
		}

		private void FrmMain_MouseUp(object sender, MouseEventArgs e)
		{
			dragging = false;
		}

		private void BtnClose_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void FrmMain_Load(object sender, EventArgs e)
		{
			var assemblyName = Path.GetFileNameWithoutExtension(GetType().Assembly.Location);

			// Ini file name = assembly name + ".ini"
			var iniFileName = assemblyName + ".ini";

			var ini = new IniFile(iniFileName);

			// Check ini
			if (!File.Exists(iniFileName))
			{
				if (MessageBox.Show(string.Format(Lang.NewIniQuery, iniFileName), Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
					Application.Exit();

				ini.Write("Window", "Width", 700);
				ini.Write("Window", "Height", 500);
				ini.Write("Window", "Background", "window.png");

				ini.Write("CloseButton", "Width", 24);
				ini.Write("CloseButton", "Height", 24);
				ini.Write("CloseButton", "X", 656);
				ini.Write("CloseButton", "Y", 20);
				ini.Write("CloseButton", "Normal", "close-normal.png");
				ini.Write("CloseButton", "Hover", "close-hover.png");
				ini.Write("CloseButton", "Down", "close-down.png");

				ini.Write("StartButton", "Width", 150);
				ini.Write("StartButton", "Height", 50);
				ini.Write("StartButton", "X", 530);
				ini.Write("StartButton", "Y", 428);
				ini.Write("StartButton", "Normal", "start-normal.png");
				ini.Write("StartButton", "Hover", "start-hover.png");
				ini.Write("StartButton", "Down", "start-down.png");
				ini.Write("StartButton", "Disabled", "start-disabled.png");

				ini.Write("Status", "X", 20);
				ini.Write("Status", "Y", 445);

				ini.Write("PatchNotes", "Width", 658);
				ini.Write("PatchNotes", "Height", 348);
				ini.Write("PatchNotes", "X", 21);
				ini.Write("PatchNotes", "Y", 58);
				ini.Write("PatchNotes", "Url", "");

				ini.Write("Client", "Exe", "client.exe");
				ini.Write("Client", "Parameters", "code:1622 ver:143 logip:127.0.0.1 logport:11000 chatip:127.0.0.1 chatport:8002 setting:\"file://data/features.xml=Regular, USA\"");

				ini.Write("Packages", "Url", "");
				ini.Write("Packages", "List", "packages.txt");

				ini.Write("Features", "Bgm", "");
			}

			// Read ini
			var windowWidth = ini.ReadInt32("Window", "Width");
			var windowHeight = ini.ReadInt32("Window", "Height");
			var windowImageName = ini.Read("Window", "Background");
			var windowImagePath = Path.Combine(assemblyName, windowImageName);

			var closeWidth = ini.ReadInt32("CloseButton", "Width");
			var closeHeight = ini.ReadInt32("CloseButton", "Height");
			var closeX = ini.ReadInt32("CloseButton", "X");
			var closeY = ini.ReadInt32("CloseButton", "Y");
			var closeNormalName = ini.Read("CloseButton", "Normal");
			var closeHoverName = ini.Read("CloseButton", "Hover");
			var closeDownName = ini.Read("CloseButton", "Down");
			var closeNormalPath = Path.Combine(assemblyName, closeNormalName);
			var closeHoverPath = Path.Combine(assemblyName, closeHoverName);
			var closeDownPath = Path.Combine(assemblyName, closeDownName);

			var startWidth = ini.ReadInt32("StartButton", "Width");
			var startHeight = ini.ReadInt32("StartButton", "Height");
			var startX = ini.ReadInt32("StartButton", "X");
			var startY = ini.ReadInt32("StartButton", "Y");
			var startNormalName = ini.Read("StartButton", "Normal");
			var startHoverName = ini.Read("StartButton", "Hover");
			var startDownName = ini.Read("StartButton", "Down");
			var startDisabledName = ini.Read("StartButton", "Disabled");
			var startNormalPath = Path.Combine(assemblyName, startNormalName);
			var startHoverPath = Path.Combine(assemblyName, startHoverName);
			var startDownPath = Path.Combine(assemblyName, startDownName);
			var startDisabledPath = Path.Combine(assemblyName, startDisabledName);

			var patchWidth = ini.ReadInt32("PatchNotes", "Width");
			var patchHeight = ini.ReadInt32("PatchNotes", "Height");
			var patchX = ini.ReadInt32("PatchNotes", "X");
			var patchY = ini.ReadInt32("PatchNotes", "Y");
			var patchUrl = ini.Read("PatchNotes", "Url");

			var statusX = ini.ReadInt32("Status", "X");
			var statusY = ini.ReadInt32("Status", "Y");

			var packagesUrl = ini.Read("Packages", "Url").Trim();
			var packagesList = ini.Read("Packages", "List").Trim();
			if (!packagesUrl.EndsWith("/"))
				packagesUrl += "/";

			exeName = ini.Read("Client", "Exe");
			exeParameters = ini.Read("Client", "Parameters");

			var bgm = ini.Read("Features", "Bgm");

			// Check window size
			if (windowWidth == 0 || windowHeight == 0)
			{
				MessageBox.Show(string.Format(Lang.WindowSizeFail, iniFileName), Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				Application.Exit();
			}

			// Check images
			foreach (var check in new string[] { windowImageName, closeNormalName, startNormalName })
			{
				if (!File.Exists(Path.Combine(assemblyName, check)))
				{
					MessageBox.Show(string.Format(Lang.ImageNotFound, check), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}

			// Check optional images
			foreach (var check in new string[] { closeHoverName, closeDownName, startHoverName, startDownName, startDisabledName })
			{
				if (string.IsNullOrWhiteSpace(check))
					continue;

				if (!File.Exists(Path.Combine(assemblyName, check)))
				{
					MessageBox.Show(string.Format(Lang.ImageNotFound, check), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}

			// Set window
			Width = windowWidth;
			Height = windowHeight;
			BackgroundImage = new Bitmap(windowImagePath);

			// Set patch notes
			WebPatchNotes.Width = patchWidth;
			WebPatchNotes.Height = patchHeight;
			WebPatchNotes.Left = patchX;
			WebPatchNotes.Top = patchY;
			WebPatchNotes.Navigate(patchUrl);

			// Set close button
			BtnClose.Width = closeWidth;
			BtnClose.Height = closeHeight;
			BtnClose.Left = closeX;
			BtnClose.Top = closeY;
			BtnClose.Image = BtnClose.ImageNormal = new Bitmap(closeNormalPath);
			if (!string.IsNullOrWhiteSpace(closeHoverName)) BtnClose.ImageHover = new Bitmap(closeHoverPath);
			if (!string.IsNullOrWhiteSpace(closeDownName)) BtnClose.ImageDown = new Bitmap(closeDownPath);

			// Set start button
			BtnStart.Width = startWidth;
			BtnStart.Height = startHeight;
			BtnStart.Left = startX;
			BtnStart.Top = startY;
			BtnStart.ImageNormal = new Bitmap(startNormalPath);
			if (!string.IsNullOrWhiteSpace(startHoverName)) BtnStart.ImageHover = new Bitmap(startHoverPath);
			if (!string.IsNullOrWhiteSpace(startDownName)) BtnStart.ImageDown = new Bitmap(startDownPath);
			if (!string.IsNullOrWhiteSpace(startDisabledName)) BtnStart.ImageDisabled = new Bitmap(startDisabledPath);
			BtnStart.Enabled = false;

			// Set status
			LblStatus.BackColor = Color.Transparent;
			LblStatus.Left = statusX;
			LblStatus.Top = statusY;
			LblStatus.Text = "";

			// Patch
			LblStatus.Text = Lang.CheckingDownloads;

			// Use a task so it doesn't block
			Task.Factory.StartNew(() => DownloadPackages(packagesUrl, packagesList));

			if (File.Exists(bgm))
			{
				var media = new Media();
				media.Play(bgm, this);
			}
		}

		private void DownloadPackages(string packagesUrl, string packagesList)
		{
			// Download list
			string list = null;
			try
			{
				var wc = new WebClient();
				list = wc.DownloadString(packagesUrl + packagesList);
			}
			catch
			{
				SetStatus(Lang.ListDownloadFailed);
				return;
			}

			// Read list and queue downloads
			var dq = new DownloadQueue();

			using (var sr = new StringReader(list))
			{
				string line = null;
				while ((line = sr.ReadLine()) != null)
				{
					line = line.Trim();

					// Skip empty and comments
					if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//"))
						continue;

					var fileName = line;
					var filePath = PackageFolder + fileName;

					// Skip existing files
					if (File.Exists(filePath))
						continue;

					dq.Add(fileName, packagesUrl + fileName, filePath);
				}
			}

			if (dq.Count > 0)
			{
				dq.Progress += OnProgress;
				dq.Finished += OnFinished;
				dq.Error += OnError;
				dq.Start();
			}
			else
			{
				OnFinished();
			}
		}

		private void BtnStart_Click(object sender, EventArgs e)
		{
			if (!File.Exists(exeName))
			{
				MessageBox.Show(string.Format(Lang.FileNotFound, exeName), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var startInfo = new ProcessStartInfo();
			startInfo.FileName = exeName;
			startInfo.Arguments = exeParameters;

			Process.Start(startInfo);

			Application.Exit();
		}

		private void OnProgress(string fileName, long receivedBytes, long totalBytes, int currentNr, int totalNr)
		{
			SetStatus(Lang.DownloadProgress, fileName, receivedBytes / 1024, totalBytes / 1024, currentNr, totalNr);
		}

		private void OnFinished()
		{
			SetStart(true);
			SetStatus(Lang.DownloadsComplete);
		}

		private void OnError(string fileName)
		{
			SetStatus(Lang.DownloadFailed, fileName);
		}

		private void SetStatus(string format, params object[] args)
		{
			LblStatus.Invoke((MethodInvoker)delegate()
			{
				LblStatus.Text = string.Format(format, args);
			});
		}

		private void SetStart(bool enabled)
		{
			BtnStart.Invoke((MethodInvoker)delegate()
			{
				BtnStart.Enabled = enabled;
			});
		}
	}
}
