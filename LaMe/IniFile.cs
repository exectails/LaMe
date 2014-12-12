using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace LaMe
{
	/// <summary>
	/// Create a New INI file to store or load data
	/// </summary>
	public class IniFile
	{
		public string IniPath { get; private set; }

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		public IniFile(string path)
		{
			IniPath = Path.Combine(Directory.GetCurrentDirectory(), path);
		}

		public void Write(string section, string key, string value)
		{
			WritePrivateProfileString(section, key, value, IniPath);
		}

		public void Write(string section, string key, int value)
		{
			Write(section, key, value.ToString());
		}

		public string Read(string section, string key)
		{
			var temp = new StringBuilder(255);
			var i = GetPrivateProfileString(section, key, "", temp, 255, IniPath);

			return temp.ToString();
		}

		public int ReadInt32(string section, string key)
		{
			var val = Read(section, key);

			if (string.IsNullOrWhiteSpace(val))
				return 0;

			return Convert.ToInt32(val);
		}
	}
}
