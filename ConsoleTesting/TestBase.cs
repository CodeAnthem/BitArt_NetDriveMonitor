using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTesting
{
	public class TestBase
	{
		public bool SkipReadKey { get; set; } = false;
		private const string _name = "[Tests]";

		public TestBase()
		{
			var childName = this.GetType().Name;
			Console.WriteLine("-------------------------------------------");
			Console.WriteLine($" {_name} of {childName} ");
			Console.WriteLine("-------------------------------------------");
		}

		public void StartMessage(string action)
		{
			Console.Write($"\n {_name} {action}: ");
			if (!SkipReadKey)
			{
				Console.Write(" Press any key to start > ");
				Console.ReadKey();
			}
			else
			{
				Console.Write("Auto-");
			}
			Console.WriteLine("Started");
		}

		public void CompleteMessage(string action)
		{
			Console.WriteLine($" {_name} {action}: Completed.\n");
		}

		public static void StopApp()
		{
			Console.WriteLine("-------------------------------------------");
			Console.WriteLine($" {_name} Completed.");
			Console.WriteLine("\n Press any key to exit!\n");
			Console.ReadKey();
			Environment.Exit(0);
		}
	}
}