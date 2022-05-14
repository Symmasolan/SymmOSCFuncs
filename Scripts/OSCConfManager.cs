using System;
using System.Configuration;

namespace SymmOSCFuncs
{
	class OSCConfManager
	{
		public string ConfLocation;
		public int ConfDaysToLog;

		// Read in the required values from the config
		public OSCConfManager()
		{
			// Store the location given in the config file
			ConfLocation = ConfigurationManager.AppSettings.Get("Location");
			// Attempt to store the number given in the config file
			int.TryParse(ConfigurationManager.AppSettings.Get("DaysToLog"), out int ConfDaysToLog);
			// Make sure the number is positive
			ConfDaysToLog = Math.Abs(ConfDaysToLog);
		}

		public string GetLocation()
		{
			return ConfLocation;
		}

		public int GetDaysToLog()
		{
			if (ConfDaysToLog > 3)
			{
				return ConfDaysToLog;
			}
			else
			{
				return 3;
			}
		}
	}
}
