using System;
using System.IO;

namespace SymmOSCFuncs
{
	/// <summary>
	/// Logging and console printing functionality
	/// </summary>
	static internal class Logging
	{
		// Define the config manager
		private static OSCConfManager confManager = new OSCConfManager();

		/// <summary>
		/// Initialize a new line for the log, recording the start time.
		/// Creates a new Logs directory if it does not already exist.
		/// </summary>
		static public void LogInit()
		{
			try
			{
				// Find the log directory, making sure it actually exists.
				string fileName = GetLogDirectory();
				// If it does not exist, create the Log directory
				if (!File.Exists(fileName))
				{
					Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, @".\Logs\"));
				}

				// Check all files to make sure none are older than 5 days
				string currentDate = DateTime.Today.ToString("yyyy-MM-dd");
				string logFolder = @".\Logs";
				foreach (string fName in Directory.GetFiles(logFolder))
				{
					int iOf = fName.IndexOf("log-oscfuncs-") + "log-oscfuncs-".Length;
					DateTime fileDate;
					int daysToLog = confManager.GetDaysToLog();
					if (DateTime.TryParse(fName.Substring(iOf, currentDate.Length), out fileDate) &&
						fileDate <= DateTime.Today.AddDays(daysToLog*-1))
					{
						PrintConsole(fileDate.ToString());
						PrintConsole($"Deleting log file \"{fName}\"\n  as it is over {daysToLog} days old!", LogLevel.INFO, ConsoleColor.Cyan);
						File.Delete(fName);
					}
				}

				// Build a starting log message
				string toLog = $"==========================================================\n" +
					$"    Start of log: {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:ffff")}\n";
				// Save it to the end of the log file
				File.AppendAllText(fileName, toLog);
				// Print it to the Log window
				OSCConsole.MessageConsole(OSCConsole.logConsole, toLog, ConsoleColor.White);
			}
			catch (Exception ex)
			{
				OSCConsole.MessageConsole(OSCConsole.logConsole, $"Logging Exception caught:\n{ex}", ConsoleColor.Red);
			}
		}

		/// <summary>
		/// Print a message to the console window. 
		/// Optionally saves the message to the Log if a LogLevel above NONE is provided.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="logLevel">Only functions if a LogLevel above NONE is provided</param>
		static public void PrintConsole(string msg, LogLevel logLevel = LogLevel.NONE, ConsoleColor customColor = ConsoleColor.White)
		{
			ConsoleColor msgColor = customColor;

			// If there's a log level..
			if (logLevel > LogLevel.NONE)
			{
				
				// Change the color based on the LogLevel if a custom color isn't defined.
				switch (logLevel)
				{
					case LogLevel.DEBUG: msgColor = ConsoleColor.Cyan; break;
					case LogLevel.WARNING: msgColor = ConsoleColor.Yellow; break;
					case LogLevel.ERROR: msgColor = ConsoleColor.Red; break;
					//case LogLevel.INFO: msgColor = ConsoleColor.White; break;
				}

				// If the message is of LogLevel.DEBUG, only print when in DEBUG
				if (logLevel == LogLevel.DEBUG)
				{
#if !DEBUG
					return;
#endif
				}

				// Write the message to the main console as well
				OSCConsole.MessageConsole(OSCConsole.mainConsole, msg, msgColor);
				// Write the message to the log
				Log(logLevel, msg);
			}
			else
			{
				// Just write the message to the main console
				OSCConsole.MessageConsole(OSCConsole.mainConsole, msg, msgColor);
			}
		}

		/// <summary>
		/// Adds a new line to the end of the log file
		/// </summary>
		/// <param name="logLevel"></param>
		/// <param name="logMsg"></param>
		static public void Log(LogLevel logLevel, string logMsg)
		{
			try
			{
				string fileName = GetLogDirectory();
				// Build a log message
				string logInfo = $"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:ffff")}] [{LogLevelToString(logLevel)}]";
				string toLog = $"{logMsg,7}\n";
				// Save it to the end of the log file
				File.AppendAllText(fileName, $"{logInfo} {toLog}");
				// Print it to the Log window
				OSCConsole.MessageConsole(OSCConsole.logConsole, $"{logInfo}\n{toLog,-7}", ConsoleColor.White);
			}
			catch (Exception ex)
			{
				OSCConsole.MessageConsole(OSCConsole.logConsole, $"Logging Exception caught:\n{ex}", ConsoleColor.Red);
			}
		}

		/// <summary>
		/// Find the current working directory and log file
		/// </summary>
		/// <returns></returns>
		static public string GetLogDirectory()
		{
			string currentDate = DateTime.Today.ToString("yyyy-MM-dd");
			string fileName = @$".\Logs\log-oscfuncs-{currentDate}.txt";

			return fileName;
		}

		static public string LogLevelToString(LogLevel logLevel)
		{
			switch (logLevel)
			{
				case LogLevel.INFO:
					return "INFO";

				case LogLevel.DEBUG:
					return "DEBUG";

				case LogLevel.WARNING:
					return "WARN";

				case LogLevel.ERROR:
					return "ERROR";

				case LogLevel.NONE:
				default:
					return "NONE";
			}
		}

		/// <summary>
		/// Log levels for displaying a string in the log file
		/// NONE = -1, INFO = 0, DEBUG = 1, ERROR = 2
		/// </summary>
		public enum LogLevel
		{
			NONE = -1,
			INFO = 0,
			DEBUG = 1,
			WARNING = 2,
			ERROR = 3
		}
	}
}
