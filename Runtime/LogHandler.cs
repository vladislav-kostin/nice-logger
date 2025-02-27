#if NICE_LOGGER_OVERRIDE_DEBUG
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NiceLogger
{
	public class LogHandler : ILogHandler
	{
		private readonly Func<Color>[] _colorGetters =
		{
			() => LoggerSettings.Settings.ErrorPrefixColor, // Error
			() => LoggerSettings.Settings.ErrorPrefixColor, // Assert
			() => LoggerSettings.Settings.WarningPrefixColor, // Warning
			() => LoggerSettings.Settings.LogPrefixColor, // Log
			() => LoggerSettings.Settings.ErrorPrefixColor // Exception
		};

		private readonly string[] _prefixes =
		{
			"ERROR",
			"ASSERTION FAILED",
			"WARNING",
			"LOG",
			"EXCEPTION"
		};

		public LogHandler(ILogHandler defaultHandler)
		{
			DefaultLogHandler = defaultHandler;
		}

		public static ILogHandler DefaultLogHandler { get; private set; }
		public static ILogHandler CustomLogHandler { get; private set; }

		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			var message = string.Format(format, args);

			message = Logger.FormatMessage(_prefixes[(int)logType], message, _colorGetters[(int)logType]());

			DefaultLogHandler.LogFormat(logType, context, message);
		}

		public void LogException(Exception exception, Object context)
		{
			var message = Logger.FormatMessage(_prefixes[(int)LogType.Exception], exception.Message, _colorGetters[(int)LogType.Exception]());
			DefaultLogHandler.LogException(exception, context);
		}

		[InitializeOnLoadMethod]
		public static void Initialize()
		{
			Debug.Log("Initialize CustomLogHandler");
			CustomLogHandler = new LogHandler(Debug.unityLogger.logHandler);
			Debug.unityLogger.logHandler = CustomLogHandler;
		}
	}
}
#endif