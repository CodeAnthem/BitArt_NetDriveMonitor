using NetDriveMonitor;
using System;

namespace UI_Console
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Console.WriteLine("Monitor Test");

			var ndm = new NDMCore();
			ndm.Start();

			Console.WriteLine("Monitor Test Done");
			Console.ReadKey();
		}
	}
}