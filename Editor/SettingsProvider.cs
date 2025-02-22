using UnityEditor;
using VK.Logger;

namespace VK.Bootstrap
{
	public static class LoggerSettingsProvider
	{
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
					settings.RegularColor = EditorGUILayout.ColorField("Regular Color", settings.RegularColor);
					settings.DebugColor = EditorGUILayout.ColorField("Debug Color", settings.DebugColor);
					settings.WarningColor = EditorGUILayout.ColorField("Warning Color", settings.WarningColor);
					settings.ErrorColor = EditorGUILayout.ColorField("Error Color", settings.ErrorColor);
					settings.ImportantColor = EditorGUILayout.ColorField("Important Color", settings.ImportantColor);
					settings.HighlightColor = EditorGUILayout.ColorField("Highlight Color", settings.HighlightColor);
					settings.MetadataColor = EditorGUILayout.ColorField("Metadata Color", settings.MetadataColor);

					if (EditorGUI.EndChangeCheck())
					{
						LoggerSettings.SaveSettings();
					}
				}
			};
		}
	}
}