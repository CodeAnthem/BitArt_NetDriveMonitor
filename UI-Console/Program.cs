using BitArt_Netdrive_Helpers;
using BitArt_Netdrive_Helpers.tests;
using System;

namespace UI_Console
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			// ------------------------------------
			//var core = new NDMCore();
			//var cmd = new ConsoleCmds(core);

			//cmd.Start(); // start the tool
			//cmd.HoldAndQuit(); // hold screen
			// ------------------------------------

			// DEBUG
			// ------------------------------------
			Console.WriteLine("Tests: Started.\n");
			// ------------------------------------

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

			//test.AddDrives();

			//[Benchmark]
			test.GetAllNetworkDrivesViaNetCMD_Test();

			//test.RemoveDrives();
			//test.AddDrives();
			//test.RemoveAll();
			// ------------------------------------

			// ------------------------------------
			Console.WriteLine("Tests: Completed.");
			Console.WriteLine("\nPress any key to exit!\n");
			Console.ReadKey();
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