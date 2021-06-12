using NetDriveMonitor;
using NetDriveMonitor.interfaces;
using System;
using System.Collections.Generic;

namespace UI_Console
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			var core = new NDMCore();
			var cmd = new ConsoleCmds(core);

			var netDrives = cmd.GetData();
			//cmd.SaveData(netDrives);
			cmd.ReadDrives(netDrives);
			cmd.Start();

			cmd.Quit();
		}
	}
}