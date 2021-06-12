using NetDriveMonitor;
using System;

namespace UI_Console
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			var core = new NDMCore();

			// get drives
			var drives = core.DataStore.Get();
			if (drives?.Count > 0)
			{
				Console.WriteLine("Drives found: " + drives?.Count);
			}
			else
			{
				Console.WriteLine("Drives found: none");
			}

			//core.DataStore.Save(drives); // save test
			//Monitor.StartMonitoring(drives);

			Console.WriteLine("\n");
			Console.WriteLine("-------------------------------------");
			Console.WriteLine("done - press any key to exit");
			Console.WriteLine("-------------------------------------");
			Console.ReadLine();
		}
	}
}