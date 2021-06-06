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

			var drives = monitor.DataStore.Get();
			foreach (var d in drives)
			{
				Console.WriteLine($"Drive: {d.Letter} {d.Server} {d.Share} - {d.VPNServer}");
			}

			Console.ReadLine();
		}
	}
}