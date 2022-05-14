using Konsole;
using System;
using static System.ConsoleColor;

namespace SymmOSCFuncs
{
	static internal class OSCConsole
	{
		private static ConcurrentWriter window = null;
		public static IConsole mainConsole = null;
		public static IConsole logConsole = null;

		public static void CreateConsoles()
		{
			window = new Window(132, 35, ConsoleColor.DarkGreen, ConsoleColor.Black).Concurrent();
			mainConsole = window.SplitLeft("MAIN", LineThickNess.Double);
			logConsole = window.SplitRight("LOG", LineThickNess.Double);
		}

		public static void MessageConsole(IConsole con, string newMsg, ConsoleColor color)
		{
			con.WriteLine(color, $"{newMsg}");
		}
    }
}
