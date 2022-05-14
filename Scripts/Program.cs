using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace SymmOSCFuncs
{
	class Program
	{
		// For preventing the user from resizing the Console window
		// (https://social.msdn.microsoft.com/Forums/vstudio/en-US/1aa43c6c-71b9-42d4-aa00-60058a85f0eb/c-console-window-disable-resize?forum=csharpgeneral)
		private const int MF_BYCOMMAND = 0x00000000;
		//public const int SC_CLOSE = 0xF060;
		//public const int SC_MINIMIZE = 0xF020;
		private const int SC_MAXIMIZE = 0xF030;
		private const int SC_SIZE = 0xF000;//resize

		[DllImport("user32.dll")]
		private static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

		[DllImport("user32.dll")]
		private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern IntPtr GetConsoleWindow();


		// Thread object for the OSC Handler
		static Thread oscMainThread;

		static void Main(string[] args)
		{
			// Prevent the user from being able to resize the Console window
			IntPtr handle = GetConsoleWindow();
			IntPtr sysMenu = GetSystemMenu(handle, false);

			if (handle != IntPtr.Zero)
			{
				//DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND);
				//DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
				DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
				DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);// Resizing
			}

			// Set the size of the console window (windows only?)
			Console.SetWindowSize(132, 42);
			Console.SetBufferSize(132, 43);// +1 Width prevents visual issue when minimizing

			// Fun lil' ascii art thing (https://textkool.com/en/ascii-art-generator?font=AMC%203%20Line)
			string asciiArt = "         ╔═══════════════════════════════════════════════════════╗\n" +
							  "         ║ .-. . . .  . .  .   .-. .-. .-.   .-. . . . . .-. .-. ║\n" +
							  "         ║ `-.  |  |\\/| |\\/|   | | `-. |     |-  | | |\\| |   `-. ║\n" +
							  "         ║ `-'  `  '  ` '  `   `-' `-' `-'   '   `-' ' ` `-' `-' ║\n" +
							  "         ╚═══════════════════════════════════════════════════════╝\n";
			Console.Write(asciiArt);
			// Setup the console window(s)
			OSCConsole.CreateConsoles();
			// Send a greeting message
			Logging.PrintConsole("You can stop this program at any time by pressing \"CTRL + C\",\n    closing the window normally, or using the \"exit\" command.\n");
			Logging.PrintConsole("Initializing Symm OSC Funcs...");
			// Init Log
			Logging.LogInit();

			// Make sure the Secrets folder exists
			SecretHandler.CheckSecretsDir();

#if DEBUG
			Logging.PrintConsole("This is a general, no LogLevel message");
			Logging.PrintConsole("This is a plain info message", Logging.LogLevel.INFO);
			Logging.PrintConsole("This is a nice DEBUG message!", Logging.LogLevel.DEBUG);
			Logging.PrintConsole("Uh oh, it's a warning!", Logging.LogLevel.WARNING);
			Logging.PrintConsole("AAAAAAAAAA ERROR", Logging.LogLevel.ERROR);
#endif

			// No longer eats the main thread, yay!
			OSCHandler oscHandler = new OSCHandler();
			oscMainThread = new Thread(oscHandler.InitOSC);
			oscMainThread.Start();

			// Takes commands as needed
			// Now *this* one eats the main thread for input!
			CommandHandler.Init(oscHandler);
		}
	}
}