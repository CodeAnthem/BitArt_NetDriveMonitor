using NetDriveMonitor;
using NetDriveMonitor.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI_Console
{
	public class ConsoleCmds
	{
		private NDMCore _core;

		public ConsoleCmds(NDMCore core)
		{
			_core = core;
		}

		public List<INetDrive> GetData()
		{
			List<INetDrive> netDrives = _core.DataStore.Get();

			if (netDrives?.Count > 0)
			{
				Console.WriteLine("Drives found: " + netDrives?.Count);
			}
			else
			{
				Console.WriteLine("Drives found: none");
			}
			return netDrives;
		}

		public void SaveData(List<INetDrive> netDrives)
		{
			_core.DataStore.Save(netDrives); // save test
			if (netDrives?.Count > 0)
			{
				Console.WriteLine("Saved {0} drives", netDrives.Count);
			}
			else
			{
				Console.WriteLine("Saved 0 drives");
			}
		}

		public void Start()
		{
			bool state = _core.Start();
			if (state)
			{
				Console.WriteLine("Started");
			}
			else
			{
				Console.WriteLine("Already running");
			}
		}

		public void Stop()
		{
			bool state = _core.Stop();
			if (state)
			{
				Console.WriteLine("Stopped");
			}
			else
			{
				Console.WriteLine("Already stopped");
			}
		}

		public void HoldAndQuit()
		{
			Console.WriteLine("\n");
			Console.WriteLine("-------------------------------------");
			Console.WriteLine("done - press any key to exit");
			Console.WriteLine("-------------------------------------");
			Console.ReadLine();
		}
	}
}