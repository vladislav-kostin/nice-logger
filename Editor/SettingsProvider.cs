using System.Linq;
using UnityEditor;

namespace NiceLogger.Editor
{
	public static class LoggerSettingsProvider
	{
		private const string DefineSymbol = "NICE_LOGGER_OVERRIDE_DEBUG";

		[SettingsProvider]
		public static SettingsProvider CreateBootstrapSettingsProvider()
		{
			return new SettingsProvider("Project/LoggerSettings", SettingsScope.Project)
			{
				label = "Logger Settings",
				guiHandler = searchContext =>
				{
					var settings = LoggerSettings.Settings;

					EditorGUI.BeginChangeCheck();
					settings.RegularMessageColor = EditorGUILayout.ColorField("Regular Message Color", settings.RegularMessageColor);
					settings.TempMessageColor = EditorGUILayout.ColorField("Temp Mesage Color", settings.TempMessageColor);
					settings.TempPrefixColor = EditorGUILayout.ColorField("Temp Prefix Color", settings.TempPrefixColor);
					settings.WarningPrefixColor = EditorGUILayout.ColorField("Warning Prefix Color", settings.WarningPrefixColor);
					settings.ErrorPrefixColor = EditorGUILayout.ColorField("Error Prefix Color", settings.ErrorPrefixColor);
					settings.ImportantPrefixColor = EditorGUILayout.ColorField("Important Prefix Color", settings.ImportantPrefixColor);
					settings.LogPrefixColor = EditorGUILayout.ColorField("Log Prefix Prefix Color", settings.LogPrefixColor);
					settings.HighlightColor = EditorGUILayout.ColorField("Highlight Color", settings.HighlightColor);
					settings.MetadataColor = EditorGUILayout.ColorField("Metadata Color", settings.MetadataColor);
					// EditorGUILayout.Space();
					// settings.OverrideDebugClass = EditorGUILayout.Toggle("Override Debug Class", settings.OverrideDebugClass);

					if (EditorGUI.EndChangeCheck())
					{
						// UpdateDefineSymbol(settings.OverrideDebugClass);
						LoggerSettings.SaveSettings();
					}
				}
			};
		}

		private static void UpdateDefineSymbol(bool shouldAdd)
		{
			var targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
			var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);

			if (shouldAdd && !defines.Contains(DefineSymbol))
			{
				defines = string.IsNullOrEmpty(defines) ? DefineSymbol : $"{defines};{DefineSymbol}";
				PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, defines);
			}
			else if (!shouldAdd && defines.Contains(DefineSymbol))
			{
				defines = string.Join(";", defines.Split(';').Where(d => d != DefineSymbol));
				PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, defines);
			}
		}
	}
}