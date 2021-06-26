using BitArt_Netdrive_Helpers.tests;
using BitArt_Network_Helpers;
using NetDriveMonitor;
using NetDriveMonitor.interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UI_Console
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			// ------------------------------------
			Console.WriteLine("Tests: Started.\n");
			// ------------------------------------

			//var core = new NDMCore();
			//var cmd = new ConsoleCmds(core);

			//cmd.Start(); // start the tool
			//cmd.HoldAndQuit(); // hold screen

			// ------------------------------------
			// Testing Net drive mapper
			// ------------------------------------
			//HostMonitor monitor = new HostMonitor();
			//monitor.NotifyOnChangesOnly = false;
			//monitor.NotifyOnFirstPing = true;
			//monitor.ScanInterval = 500;
			//monitor.OnHostChanged += HostUpdated;

			//monitor.AddHost("dp-nas10");
			//monitor.AddHost("dp-nas10.dp");

			//monitor.Start();
			//Console.WriteLine("Monitoring {0} targets: ", monitor.HostCount);
			// ------------------------------------

			// ------------------------------------
			// Testing Net drive mapper
			// ------------------------------------
			var drives = NetdriveGenerator.GetDummyData();
			var test = new NetdriveMapperTest(drives);
			test.AddDrives();
			test.RemoveDrives();
			test.AddDrives();
			test.RemoveAll();
			// ------------------------------------

			// ------------------------------------
			Console.WriteLine("Tests: Completed.");
			Console.WriteLine("\nPress any key to continue!\n");
			Console.ReadLine();
			// ------------------------------------
		}

		private static void HostUpdated(string hostName, bool state)
		{
			if (state)
			{
				Console.WriteLine("Host {0} is availible", hostName);
			}
			else
			{
				Console.WriteLine("Host {0} is unavailible", hostName);
			}
		}
	}
}