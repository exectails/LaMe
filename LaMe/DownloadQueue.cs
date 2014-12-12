using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace LaMe
{
	public class DownloadQueue
	{
		private const string TmpSuffix = ".dqd";

		private List<Download> downloads = new List<Download>();
		private int current = -1;

		/// <summary>
		/// Raised when progress of a file download changes.
		/// </summary>
		public event Action<string, long, long, int, int> Progress;

		/// <summary>
		/// Raised when all downloads are finished.
		/// </summary>
		public event Action Finished;

		/// <summary>
		/// Raised on download error.
		/// </summary>
		public event Action<string> Error;

		/// <summary>
		/// Number of downloads in the queue
		/// </summary>
		public int Count { get { return downloads.Count; } }

		/// <summary>
		/// Adds to download queue.
		/// </summary>
		/// <param name="name">Reference file name</param>
		/// <param name="url">Full url to the file</param>
		/// <param name="localPath">Local target path</param>
		public void Add(string name, string url, string localPath)
		{
			downloads.Add(new Download(name, url, localPath));
		}

		/// <summary>
		/// Starts queued downloads.
		/// </summary>
		public void Start()
		{
			current = 0;
			Next();
		}

		/// <summary>
		/// Dowloads next file.
		/// </summary>
		private void Next()
		{
			var download = downloads[current];

			// Create parent folder if it doesn't exist
			var parentFolder = Path.GetDirectoryName(download.Local);
			if (!Directory.Exists(parentFolder))
				Directory.CreateDirectory(parentFolder);

			// Start download
			var wc = new WebClient();
			wc.DownloadProgressChanged += OnDownloadProgressChanged;
			wc.DownloadFileCompleted += OnDownloadFileCompleted;
			wc.DownloadFileAsync(new Uri(download.Url), download.Local + TmpSuffix);
		}

		/// <summary>
		/// Called when one file download completed or failed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			var download = downloads[current];

			if (e.Error != null)
			{
				if (Error != null)
					Error(download.Name);

				return;
			}

			File.Move(download.Local + TmpSuffix, download.Local);

			if (current >= downloads.Count - 1)
			{
				if (Finished != null)
					Finished();

				return;
			}

			current++;
			Next();
		}

		/// <summary>
		/// Called when download progress of one file changes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			if (Progress != null)
				Progress(downloads[current].Name, e.BytesReceived, e.TotalBytesToReceive, current + 1, Count);
		}

		/// <summary>
		/// Download information
		/// </summary>
		private class Download
		{
			public Download(string name, string url, string local)
			{
				Name = name;
				Url = url;
				Local = local;
			}

			public string Name { get; private set; }
			public string Url { get; private set; }
			public string Local { get; private set; }
		}
	}
}
