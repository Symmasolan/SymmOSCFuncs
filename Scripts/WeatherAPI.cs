using System;

namespace SymmOSCFuncs
{
	class WeatherAPI
	{
		public static DateTime lastAstroUpdate = DateTime.UnixEpoch;
		public static WeatherAPIAstronomyJson.Root lastAstroData;

		public static WeatherAPIAstronomyJson.Root GetAstroAPIData(string locationOrZipcode = "London")
		{
			// Check if it's been at least X minutes since the last update request
			int checkBuffer = 5;// minutes
			if (lastAstroData != null && (DateTime.Now < lastAstroUpdate.AddMinutes(checkBuffer)))
			{
				//Console.WriteLine("TOO EARLY SINCE LAST UPDATE!");
				Logging.PrintConsole("Hasn't been more than 5 minutes- returning cached data!", Logging.LogLevel.INFO);
				return lastAstroData;
			}
			else
			{
				try
				{
					// Set our last update time to the current time
					lastAstroUpdate = DateTime.Now;

					// Grab an update from the API, then store and return the result.
					string astroURL = BuildAstronomyAPIURL(locationOrZipcode);

					WeatherAPIAstronomyJson.Root jsonResult = Networking.RequestAstroAPI(astroURL);
					// Store it
					lastAstroData = jsonResult;

					// Return it
					return jsonResult;
				}
				catch (Exception ex)
				{
					OSCConsole.MessageConsole(OSCConsole.logConsole, $"WeatherAPI Exception caught:\n{ex}", ConsoleColor.Red);
					return null;
				}
				
			}
		}

		public static string BuildAstronomyAPIURL(string location = "London")
		{
			SecretHandler weatherSecret = new SecretHandler();
			string currentDay = DateTime.Now.ToString("yyyy-MM-dd");
			string astroURL = String.Format("https://api.weatherapi.com/v1/astronomy.json?key={0}&q={1}&dt={2}", weatherSecret.GetWeatherapiKey(), location, currentDay);

			return astroURL;
		}

		public static MoonPhases MoonPhaseToEnum(string phaseName)
		{
			// Default to 0
			MoonPhases result = MoonPhases.NewMoon;

			switch (phaseName)
			{
				case "New Moon":
					result = MoonPhases.NewMoon;
					break;
				case "Waxing Crescent":
					result = MoonPhases.WaxingCrescent;
					break;
				case "First Quarter":
					result = MoonPhases.FirstQuarter;
					break;
				case "Waxing Gibbous":
					result = MoonPhases.WaxingGibbous;
					break;
				case "Full Moon":
					result = MoonPhases.FullMoon;
					break;
				case "Waning Gibbous":
					result = MoonPhases.WaningGibbous;
					break;
				case "Third Quarter":
					result = MoonPhases.ThirdQuarter;
					break;
				case "Waning Crescent":
					result = MoonPhases.WaningCrescent;
					break;
				default:
					// Already set to 0 by default, so just warn the user.
					Console.WriteLine($"WARNING: Moon Phase \"{phaseName}\" not recognized! Defaulting with moon phase NewMoon(0)");
					break;
			}

			return result;
		}
	}

	public class WeatherAPIAstronomyJson
	{
		public class Location
		{
			public string name { get; set; }
			public string region { get; set; }
			public string country { get; set; }
			public double lat { get; set; }
			public double lon { get; set; }
			public string tz_id { get; set; }
			public int localtime_epoch { get; set; }
			public string localtime { get; set; }
		}

		public class Astro
		{
			public string sunrise { get; set; }
			public string sunset { get; set; }
			public string moonrise { get; set; }
			public string moonset { get; set; }
			public string moon_phase { get; set; }
			public string moon_illumination { get; set; }
		}

		public class Astronomy
		{
			public Astro astro { get; set; }
		}

		public class Root
		{
			public Location location { get; set; }
			public Astronomy astronomy { get; set; }
		}
	}

	public enum MoonPhases
	{
		NewMoon = 1,
		WaxingCrescent = 2,
		FirstQuarter = 3,
		WaxingGibbous = 4,
		FullMoon = 5,
		WaningGibbous = 6,
		ThirdQuarter = 7,
		WaningCrescent = 8
	}
}
