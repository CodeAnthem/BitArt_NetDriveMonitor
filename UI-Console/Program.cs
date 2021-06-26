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
			//var core = new NDMCore();
			//var cmd = new ConsoleCmds(core);

			//cmd.Start(); // start the tool
			//cmd.HoldAndQuit(); // hold screen

			BAHostMonitor monitor = new BAHostMonitor();

			monitor.NotifyOnChangesOnly = false;
			monitor.NotifyOnFirstPing = true;
			monitor.ScanInterval = 500;
			monitor.OnHostChanged += HostUpdated;

			monitor.AddHost("dp-nas10");
			monitor.AddHost("dp-nas10.dp");

			monitor.Start();
			Console.WriteLine("Monitoring {0} targets: ", monitor.HostCount);

			Console.ReadLine();
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