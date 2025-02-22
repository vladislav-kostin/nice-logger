using System.Diagnostics;
using System.Reflection;
using System.Text;
using UnityEngine;
using VK.Logger;
using Debug = UnityEngine.Debug;

public static class Logger
{
	// private static string ColorWhite = "#FFFFFF";
	// private static readonly string ColorBlue = "#3498DB";
	// private static string ColorTeal = "#1ABC9C";
	// private static readonly string ColorGold = "#F1C40F";
	// private static readonly string ColorRed = "#E74C3C";
	// private static readonly string ColorOrange = "#DC7633";
	// private static readonly string ColorGrey = "#7F8C8D";

	private static LoggerSettingsData Settings => LoggerSettings.Settings;

	public static void LogDebug(object message = null)
	{
		var logMessage = FormatMessage("DEBUG", message, Settings.DebugColor);
		Debug.Log(logMessage);
	}

	public static void LogImportant(object message = null)
	{
		var logMessage = FormatMessage("IMPORTANT", message, Settings.ImportantColor);
		Debug.Log(logMessage);
	}

	public static void LogError(object message = null)
	{
		var logMessage = FormatMessage("ERROR", message, Settings.ErrorColor);
		Debug.LogError(logMessage);
	}

	public static void LogWarning(object message = null)
	{
		var logMessage = FormatMessage("WARNING", message, Settings.WarningColor);
		Debug.LogWarning(logMessage);
	}

	public static void Assert(bool expression, object message = null)
	{
		var logMessage = FormatMessage("ERROR", message, Settings.ErrorColor);
		Debug.Assert(expression, $"Assertion failed: {logMessage}");
	}

	private static string FormatMessage(string prefix, object message, Color prefixColor)
	{
		var callingMethod = GetCallingMethod();
		var className = GetActualClassName(callingMethod) ?? "UnknownClass";
		var methodName = callingMethod?.Name ?? "UnknownMethod";
		var lineNumber = GetLineNumber(callingMethod);
		var formattedMessage = ColorizeSquareBracketsContent(message?.ToString(), Settings.HighlightColor);
		var prefixColorHex = $"#{ColorUtility.ToHtmlStringRGBA(prefixColor)}";
		var regularColorHex = $"#{ColorUtility.ToHtmlStringRGBA(Settings.RegularColor)}";
		var metadataColorHex = $"#{ColorUtility.ToHtmlStringRGBA(Settings.MetadataColor)}";
		return $"<color={prefixColorHex}>[{prefix}]</color> <color={metadataColorHex}>({className}.{methodName}:{lineNumber})</color> <color={regularColorHex}>{formattedMessage}</color>";
	}

	private static MethodBase GetCallingMethod()
	{
		var stackTrace = new StackTrace();
		var frames = stackTrace.GetFrames();

		foreach (var frame in frames)
		{
			var method = frame.GetMethod();
			if (method.DeclaringType != typeof(Logger) && method.DeclaringType != typeof(Debug))
			{
				return method;
			}
		}

		return null;
	}

	private static string GetActualClassName(MethodBase method)
	{
		if (method == null)
		{
			return null;
		}

		var stackTrace = new StackTrace();
		var frames = stackTrace.GetFrames();

		foreach (var frame in frames)
		{
			var frameMethod = frame.GetMethod();
			if (frameMethod != null && frameMethod.DeclaringType != typeof(Debug) && frameMethod.DeclaringType != typeof(Logger))
			{
				return frameMethod.DeclaringType.Name;
			}
		}

		return method.DeclaringType.Name;
	}

	private static int GetLineNumber(MethodBase method)
	{
		var stackTrace = new StackTrace(true);
		var frames = stackTrace.GetFrames();

		// Find the frame corresponding to the calling method
		for (var i = 0; i < frames.Length; i++)
		{
			var frame = frames[i];
			if (IsFrameFromCallingMethod(frame, method))
			{
				return frame.GetFileLineNumber();
			}
		}

		return -1; // Line number not found
	}

	private static bool IsFrameFromCallingMethod(StackFrame frame, MethodBase method)
	{
		var frameMethod = frame.GetMethod();

		return frameMethod != null && frameMethod == method;
	}

	private static string ColorizeSquareBracketsContent(string message, Color color)
	{
		if (string.IsNullOrEmpty(message))
		{
			return message;
		}

		var result = new StringBuilder();
		var bracketLevel = 0;

		foreach (var c in message)
		{
			if (c == '[')
			{
				if (bracketLevel == 0)
				{
					result.Append($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>");
				}
				else
				{
					result.Append(c);
				}

				bracketLevel++;
			}
			else if (c == ']')
			{
				bracketLevel--;
				if (bracketLevel == 0)
				{
					result.Append("</color>");
				}
				else
				{
					result.Append(c);
				}
			}
			else
			{
				result.Append(c);
			}
		}

		return result.ToString();
	}
}