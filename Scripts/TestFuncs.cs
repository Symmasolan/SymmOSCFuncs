using System;
using Rug.Osc;

namespace SymmOSCFuncs
{
	class TestFuncs
	{
		public static void MoonTest(OscSender sender)
		{
			Logging.PrintConsole("Moon testing..", Logging.LogLevel.DEBUG);

			var apiresult = WeatherAPI.GetAstroAPIData("New York");
			int moonPhase = (int)WeatherAPI.MoonPhaseToEnum(apiresult.astronomy.astro.moon_phase);
			float moonIllumination = float.Parse(apiresult.astronomy.astro.moon_illumination) * 0.01f;

			// Pass Moon Phase (string) as ID number (1-8)
			Logging.PrintConsole($"Moon Phase: {apiresult.astronomy.astro.moon_phase} (ID: {moonPhase})", Logging.LogLevel.INFO, ConsoleColor.Cyan);
			sender.Send(new OscMessage("/avatar/parameters/OSCReceivedID", moonPhase));
			// Pass Moon Illumination (0-100) as blend tree compatible float (0-1)
			Logging.PrintConsole($"Moon Illumination: {moonIllumination}", Logging.LogLevel.INFO, ConsoleColor.Cyan);
			sender.Send(new OscMessage("/avatar/parameters/OSCReceivedData", moonIllumination));
		}
	}
}
