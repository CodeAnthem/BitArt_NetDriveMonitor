using NetDriveMonitor;
using NetDriveMonitor.models;
using System;
using System.Threading;

namespace UI_Console
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var monitor = new NTMonitor();
			var test = monitor.NDHelper.IsDriveAlreadyMapped("C:");
			Console.WriteLine("Hello World! " + test);

			Console.WriteLine();
			Console.WriteLine("Mapping Drive now..");
			Console.ReadLine();
			var drive = new NetDriveModel("H:", "dp-nas10", "home");
			var result = monitor.NDHelper.Add(drive);
			Console.WriteLine("Connection: " + result);

			Console.ReadLine();
		}
	}
}