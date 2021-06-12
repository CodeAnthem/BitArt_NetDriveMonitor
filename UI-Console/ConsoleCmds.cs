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

		public void ReadDrives(List<INetDrive> netDrives)
		{
			if (netDrives?.Count > 0)
			{
				//TODO: for each
				_core.ReadDrives(netDrives);
				Console.WriteLine("Read {0} drives", netDrives.Count);
			}
			else
			{
				Console.WriteLine("Read 0 drives, driveList was empty");
			}
		}

		public void Start()
		{
			_core.HostWatcher.Start();
			Console.WriteLine("Started");
		}

		public void Stoped()
		{
			_core.HostWatcher.Stop();
			Console.WriteLine("Stoped");
		}

		public void Quit()
		{
			Console.WriteLine("\n");
			Console.WriteLine("-------------------------------------");
			Console.WriteLine("done - press any key to exit");
			Console.WriteLine("-------------------------------------");
			Console.ReadLine();
		}
	}
}