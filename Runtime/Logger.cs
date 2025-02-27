using System.Diagnostics;
using System.Reflection;
using System.Text;
using NiceLogger;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

public static class Logger
{
	private static LoggerSettingsData Settings => LoggerSettings.Settings;

	public static void LogTemp(object message = null, Object context = null)
	{
		Debug.Log(FormatMessage("TEMP", message, Settings.TempPrefixColor, Settings.TempMessageColor), context);
	}

	public static void LogImportant(object message = null, Object context = null)
	{
		Debug.Log(FormatMessage("IMPORTANT", message, Settings.ImportantPrefixColor, Settings.RegularMessageColor), context);
	}

	public static void LogError(object message = null, Object context = null)
	{
		Debug.LogError(FormatMessage("ERROR", message, Settings.ErrorPrefixColor, Settings.RegularMessageColor), context);
	}

	public static void LogWarning(object message = null, Object context = null)
	{
		Debug.LogWarning(FormatMessage("WARNING", message, Settings.WarningPrefixColor, Settings.RegularMessageColor), context);
	}

	public static void Assert(bool expression, object message = null, Object context = null)
	{
		Debug.Assert(expression, FormatMessage("ASSERTION FAILED", message, Settings.ErrorPrefixColor, Settings.RegularMessageColor), context);
	}

	public static string FormatMessage(string prefix, object message, Color prefixColor, Color messageColor)
	{
		var callingMethod = GetCallingMethod();
		var className = GetActualClassName(callingMethod) ?? "UnknownClass";
		var methodName = callingMethod?.Name ?? "UnknownMethod";
		var lineNumber = GetLineNumber(callingMethod);
		var formattedMessage = ColorizeSquareBracketsContent(message?.ToString(), Settings.HighlightColor);
		var prefixColorHex = $"#{ColorUtility.ToHtmlStringRGBA(prefixColor)}";
		var regularColorHex = $"#{ColorUtility.ToHtmlStringRGBA(messageColor)}";
		var metadataColorHex = $"#{ColorUtility.ToHtmlStringRGBA(Settings.MetadataColor)}";
		return $"<color={prefixColorHex}>[{prefix}]</color> <color={metadataColorHex}>[{className}.{methodName}:{lineNumber}]</color> <color={regularColorHex}>{formattedMessage}</color>";
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

		for (var i = 0; i < frames.Length; i++)
		{
			var frame = frames[i];
			if (IsFrameFromCallingMethod(frame, method))
			{
				return frame.GetFileLineNumber();
			}
		}

		return -1;
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