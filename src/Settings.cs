// CivOne
//
// To the extent possible under law, the person who associated CC0 with
// CivOne has waived all copyright and related or neighboring rights
// to CivOne.
//
// You should have received a copy of the CC0 legalcode along with this
// work. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.

using System;
using System.IO;
using CivOne.Enums;

namespace CivOne
{
	public class Settings
	{
		// Set default settings
		private GraphicsMode _graphicsMode = GraphicsMode.Graphics256;
		private bool _fullScreen = false;
		private bool _rightSideBar = false;
		private int _scale = 2;
		private bool _revealWorld = false;
		
		internal string BinDirectory
		{
			get
			{
				return new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;
			}
		}
		
		internal string CaptureDirectory
		{
			get
			{
				return Path.Combine(BinDirectory, "capture");
			}
		}
		
		internal string DataDirectory
		{
			get
			{
				return Path.Combine(BinDirectory, "data");
			}
		}
		
		internal string PluginsDirectory
		{
			get
			{
				return Path.Combine(BinDirectory, "plugins");
			}
		}
		
		internal string SavesDirectory
		{
			get
			{
				return Path.Combine(BinDirectory, "saves");
			}
		}
		
		private string SettingsDirectory
		{
			get
			{
				return Path.Combine(BinDirectory, "settings");
			}
		}
		
		internal GraphicsMode GraphicsMode
		{
			get
			{
				return _graphicsMode;
			}
			set
			{
				_graphicsMode = value;
				string saveValue = _graphicsMode == GraphicsMode.Graphics256 ? "1" : "2";
				SetSetting("GraphicsMode", saveValue);
				Common.ReloadSettings = true;
			}
		}
		
		internal bool FullScreen
		{
			get
			{
				return _fullScreen;
			}
			set
			{
				_fullScreen = value;
				SetSetting("FullScreen", _fullScreen ? "1" : "0");
				Common.ReloadSettings = true;
			}
		}
		
		internal bool RightSideBar
		{
			get
			{
				return _rightSideBar;
			}
			set
			{
				_rightSideBar = value;
				SetSetting("SideBar", _rightSideBar ? "1" : "0");
				Common.ReloadSettings = true;
			}
		}
		
		internal int Scale
		{
			get
			{
				return _scale;
			}
			set
			{
				if (value < 1 || value > 4) return;
				_scale = value;
				SetSetting("Scale", _scale.ToString());
				Common.ReloadSettings = true;
			}
		}
		
		internal bool RevealWorld
		{
			get
			{
				return _revealWorld;
			}
			set
			{
				_revealWorld = value;
				SetSetting("RevealWorld", _revealWorld ? "1" : "0");
				Common.ReloadSettings = true;
			}
		}

		internal bool Animations { get; set; }
		internal bool CivilopediaText { get; set; }
		internal bool EndOfTurn { get; set; }
		internal bool InstantAdvice { get; set; }

		internal void RevealWorldCheat()
		{
			_revealWorld = !_revealWorld;
		}
		
		internal int ScaleX
		{
			get
			{
				return _scale;
			}
		}
		
		internal int ScaleY
		{
			get
			{
				return _scale;
			}
		}
		
		private static Settings _instance;
		internal static Settings Instance
		{
			get
			{
				if (_instance == null)
					_instance = new Settings();
				return _instance;
			}
		}
		
		private string GetSetting(string settingName)
		{
			string filename = Path.Combine(SettingsDirectory, settingName);
			if (!File.Exists(filename)) return null;
			using (StreamReader sr = new StreamReader(filename))
			{
				string value = sr.ReadToEnd();
				
				Console.WriteLine("Setting Loaded - {0}: {1}", settingName, value);
				
				return value.Trim();
			}
		}
		
		private void SetSetting(string settingName, string value)
		{
			string filename = Path.Combine(SettingsDirectory, settingName);
			using (StreamWriter sw = new StreamWriter(filename, false))
			{
				sw.Write(value);
				
				Console.WriteLine("Setting Saved - {0}: {1}", settingName, value);
			}
		}
		
		private void CreateDirectories()
		{
			foreach (string dir in new[] { CaptureDirectory, DataDirectory, PluginsDirectory, SavesDirectory, SettingsDirectory })
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			
			for (char c = 'a'; c <= 'z'; c++)
			{
				string dir = Path.Combine(SavesDirectory, c.ToString());
				if (!Directory.Exists(dir))
				{
					Directory.CreateDirectory(dir);
				}
			}
		}
		
		private Settings()
		{
			CreateDirectories();
			
			int graphicsMode = (int)_graphicsMode;
			bool fullScreen = _fullScreen;
			bool rightSideBar = _rightSideBar;
			int scale = _scale;
			bool revealWorld = false;
			
			// Read settings
			Int32.TryParse(GetSetting("GraphicsMode"), out graphicsMode);
			fullScreen = (GetSetting("FullScreen") == "1");
			rightSideBar = (GetSetting("SideBar") == "1");
			Int32.TryParse(GetSetting("Scale"), out scale);
			revealWorld = (GetSetting("RevealWorld") == "1");
			
			// Set settings
			if (graphicsMode > 0 && graphicsMode < 3) _graphicsMode = (GraphicsMode)graphicsMode;
			_fullScreen = fullScreen;
			_rightSideBar = rightSideBar;
			if (scale < 1 || scale > 4) scale = 2;
			_scale = scale;
			_revealWorld = revealWorld;

			// Set game options
			EndOfTurn = false;
		}
	}
}