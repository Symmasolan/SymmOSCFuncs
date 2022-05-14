using System;
using Rug.Osc;

namespace SymmOSCFuncs
{
	internal class OSCFunctions
	{
		// Define the config manager
		private static OSCConfManager confManager = new OSCConfManager();

		public static void MoonRequest(OscSender sender)
		{
			// Send a weatherAPI request, then store the result
			string weatherLocation = confManager.GetLocation();
			Logging.PrintConsole($"Conf Location: {weatherLocation}", Logging.LogLevel.DEBUG);
			var apiresult = WeatherAPI.GetAstroAPIData(weatherLocation);
			int moonPhase = (int)WeatherAPI.MoonPhaseToEnum(apiresult.astronomy.astro.moon_phase);
			float moonIllumination = float.Parse(apiresult.astronomy.astro.moon_illumination) * 0.01f;

			// Pass Moon Phase (string) as ID number (1-8)
			Logging.PrintConsole($"Moon Phase: {apiresult.astronomy.astro.moon_phase} (ID: {moonPhase})", Logging.LogLevel.INFO, ConsoleColor.Cyan);
			sender.Send(new OscMessage("/avatar/parameters/OSCReceivedID", moonPhase));
			// Pass Moon Illumination (0-100) as compatible float (0-1)
			Logging.PrintConsole($"Moon Illumination: {moonIllumination}", Logging.LogLevel.INFO, ConsoleColor.Cyan);
			sender.Send(new OscMessage("/avatar/parameters/OSCReceivedData", moonIllumination));
		}
	}
}
