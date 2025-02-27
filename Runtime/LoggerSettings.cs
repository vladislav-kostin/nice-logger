using System;
using System.IO;
using UnityEngine;

namespace NiceLogger
{
	public static class LoggerSettings
	{
		private static readonly string SettingsPath = "ProjectSettings/LoggerSettings.json";
		private static LoggerSettingsData _settings;

		public static LoggerSettingsData Settings
		{
			get
			{
				if (_settings == null)
				{
					LoadSettings();
				}

				return _settings;
			}
		}

		public static void LoadSettings()
		{
			if (File.Exists(SettingsPath))
			{
				var json = File.ReadAllText(SettingsPath);
				_settings = JsonUtility.FromJson<LoggerSettingsData>(json);
			}
			else
			{
				_settings = new LoggerSettingsData();
				SaveSettings();
			}
		}

		public static void SaveSettings()
		{
			var json = JsonUtility.ToJson(_settings, true);
			Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath));
			File.WriteAllText(SettingsPath, json);
		}
	}

	[Serializable]
	public class LoggerSettingsData
	{
		public Color RegularMessageColor = new(.8f, .8f, .8f);
		public Color TempMessageColor = new(.8f, .8f, .5f);
		public Color TempPrefixColor = new(.8f, .5f, .2f);
		public Color WarningPrefixColor = new(.8f, .8f, .2f);
		public Color ErrorPrefixColor = new(.8f, .2f, .2f);
		public Color LogPrefixColor = new(.8f, .8f, .8f);
		public Color ImportantPrefixColor = new(.2f, .8f, .8f);
		public Color HighlightColor = new(.5f, .8f, .2f);
		public Color MetadataColor = new(.5f, .5f, .5f);
		public bool OverrideDebugClass;
	}
}