using System;
using System.IO;

namespace SymmOSCFuncs
{
	class SecretHandler
	{
		public static void CheckSecretsDir()
		{
			string filePath = Path.Combine(Environment.CurrentDirectory, @".\Secrets");
			if (!Directory.Exists(filePath))
			{
				//Logging.PrintConsole(".\\Secrets directory does not exist. Creating now!", Logging.LogLevel.DEBUG);
				Logging.PrintConsole(".\\Secrets directory does not exist. Creating now!", Logging.LogLevel.WARNING);
				Logging.PrintConsole("This warning means that this program WILL NOT WORK!", Logging.LogLevel.WARNING);
				Logging.PrintConsole("Refer to the README for more information on setup required.", Logging.LogLevel.WARNING);
				Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, @".\Secrets"));
			}
			else
			{
				Logging.PrintConsole(".\\Secrets directory already exists", Logging.LogLevel.DEBUG);
			}
		}

		public string GetWeatherapiKey()
		{
			return readSecretsFile("weatherapi.key");
		}

		// TODO: (Maybe if needed?) Abstract further into a generalized file reader, not just Secrets-specific
		private string readSecretsFile(string filename)
		{
			try
			{
				// Navigate to the current working project folder
				string fileName = Path.Combine(Environment.CurrentDirectory, @$".\Secrets\{filename}");
				if (!File.Exists(fileName))
				{
					Logging.PrintConsole("Error: requested file from \".\\Secrets\\\" does not exist! ", Logging.LogLevel.ERROR);
					return null;
				}
				//Logging.PrintConsole($"Filepath: {fileName}", Logging.LogLevel.DEBUG);
				// Open a stream reader and read in the contents, then close the reader.
				StreamReader reader = new StreamReader(fileName);
				string fileContents = reader.ReadToEnd();
				reader.Close();

				// Only return the first element before any line breaks
				string[] fileLines = fileContents.Split("\n"[0]);
				return fileLines[0];
			}
			catch (Exception ex)
			{
				OSCConsole.MessageConsole(OSCConsole.logConsole, $"SecretsHandler Exception caught:\n{ex}", ConsoleColor.Red);
				return null;
			}
		}
	}
}
