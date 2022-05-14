using System;
using System.Net;
using System.Threading;
using System.Diagnostics;
using Rug.Osc;

namespace SymmOSCFuncs
{
	class OSCHandler
	{
		static Thread oscThread;
		private static OscSender sender;
		private static OscReceiver receiver;

		// TODO: The functionality behind this does not work and I don't know why :)
		public string forceUpdate = "";

		public void InitOSC()
		{
			// Looping check to see if VRChat/Unity is running
			bool VRCIsRunning = false;
			Logging.PrintConsole("Scanning for OSC app... (VRChat, Unity)", Logging.LogLevel.INFO);

			while (VRCIsRunning == false)
			{
				Process[] vrcCheck = Process.GetProcessesByName("VRChat");
				// Support for Avatar Emulator OSC debugging/testing
				Process[] unityCheck = Process.GetProcessesByName("Unity");

				// Is VRC running?
				if (vrcCheck.Length > 0 || unityCheck.Length > 0)
				{
					// Yes
					VRCIsRunning = true;
					Logging.PrintConsole("OSC app found!", Logging.LogLevel.INFO);
				}
				else
				{
					// Delay between checks
					Thread.Sleep(5000);
				}
			}

			// Delay before starting the receiver- Ensures VRC's OSC system is started up
			Thread.Sleep(5000);// 1000 = 1 second

			// Create a new OscSender to send packages
			sender = new OscSender(IPAddress.Loopback, 0, 9000);
			// Connect the sender with given port and IP
			sender.Connect();

			// Create a receiver
			receiver = new OscReceiver(9001);
			// Create a thread to do the listening
			oscThread = new Thread(new ThreadStart(ListenLoop));
			// Connect the sender with given port
			receiver.Connect();
			// Start the listen thread
			oscThread.Start();

			Logging.PrintConsole("OSC Receiver started", Logging.LogLevel.INFO);
		}

		// ListenLoop from https://bitbucket.org/rugcode/rug.osc/wiki/Receiving%20a%20message
		private void ListenLoop()
		{
			try
			{
				while (receiver.State != OscSocketState.Closed)
				{
					// if we are in a state to recieve
					if (receiver.State == OscSocketState.Connected)
					{
						// get the next message
						// this will block until one arrives or the socket is closed
						OscPacket packet = receiver.Receive();

						// Seperate the packet information
						string packetParam = packet.ToString().Substring(0, packet.ToString().IndexOf(", "));
						string packetVal = packet.ToString().Substring(packet.ToString().IndexOf(", ") + 2);

						// Write the packet to the console for debugging
						Logging.PrintConsole($"{packetParam}, {packetVal}", Logging.LogLevel.DEBUG);

						

						// DO SOMETHING HERE!
						if (forceUpdate == "moon" || (packetParam == "/avatar/parameters/MoonUpdate" && packetVal == "True"))
						{
							Logging.PrintConsole("==========", Logging.LogLevel.DEBUG);
							Logging.PrintConsole("Moon Data update requested", Logging.LogLevel.INFO);
							Logging.PrintConsole($"{packetParam}, {packetVal}", Logging.LogLevel.DEBUG);
							OSCFunctions.MoonRequest(sender);
							Logging.PrintConsole("==========", Logging.LogLevel.DEBUG);
						}
					}

					if (forceUpdate != "")
					{
						Logging.PrintConsole($"forceUpdate: {forceUpdate}", Logging.LogLevel.DEBUG);
						// Clear the forceUpdate value each loop
						forceUpdate = "";
					}

				}
			}
			catch (Exception ex)
			{
				// Spit out the error message
				Logging.PrintConsole($"Exception in OSC listen loop:\n{ex}", Logging.LogLevel.ERROR);
				Logging.PrintConsole($"PRESS ANY KEY TO EXIT...");
				Console.ReadKey();
			}
		}
	}
}
