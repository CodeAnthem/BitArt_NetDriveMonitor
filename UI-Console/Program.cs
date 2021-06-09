using NetDriveMonitor;
using System;

namespace UI_Console
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			var core = new NDMCore();
			core.Start(); // start monitor watch dog

			Console.WriteLine("\n");
			Console.WriteLine("-------------------------------------");
			Console.WriteLine("done - press any key to exit");
			Console.WriteLine("-------------------------------------");
			Console.ReadLine();
		}
	}
}