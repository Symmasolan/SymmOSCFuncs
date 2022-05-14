using System;

namespace SymmOSCFuncs
{
	static internal class CommandHandler
	{
		public static void Init(OSCHandler oscHandler)
		{
			while (true)
			{
				// Start taking input
				string newCommand = NewCommand();
				HandleCommand(newCommand, oscHandler);

				// Remove the previous input line
				Console.SetCursorPosition(0, 40);
				for (int i = 0; i < Console.BufferWidth; i++)
				{
					Console.Write('\u0000');
				}
			}
		}

		private static string NewCommand()
		{
			Console.SetCursorPosition(0, 40);
			Console.Write(">");
			return Console.ReadLine();
		}

		private static void HandleCommand(string inputCmd, OSCHandler oscHandler)
		{
			// Log all commands entered
			Logging.Log(Logging.LogLevel.INFO, $"Command entered: \"{inputCmd.ToLower()}\"");

			switch (inputCmd.ToLower())
			{
				case "help":
					string helpStr1 = "Commands:\n" +
									 " \"help\"\n" +
									 "   - This command! Displays information about all available commands.\n" +
									 " \"forceupdate moon\"\n" +
									 "   - Forces MoonUpdate value to be \"true\", running a Weather API call.\n" +
									 " \"exit\"\n" +
									 "   - Closes the program.";

					Logging.PrintConsole(helpStr1, Logging.LogLevel.NONE, ConsoleColor.Cyan);
					break;

				case "forceupdate moon":
					Logging.PrintConsole("TODO: Fix this command lmao", Logging.LogLevel.NONE, ConsoleColor.Cyan);
					//Logging.PrintConsole("Forcing MoonUpdate.\n  Value will update only if OSC Receiver is connected!", Logging.LogLevel.NONE, ConsoleColor.Cyan);
					//oscHandler.forceUpdate = "moon";
					break;

				case "exit":
					Environment.Exit(0);
					break;

				default:
					Logging.PrintConsole($"Command not recognized: \"{inputCmd}\"\n  Try command \"help\" for more commands.", Logging.LogLevel.NONE, ConsoleColor.Cyan);
					break;
			}
		}
	}
}
