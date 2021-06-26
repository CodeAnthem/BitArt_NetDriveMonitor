using NetDriveMonitor.components;
using NetDriveMonitor.interfaces;
using NetDriveMonitor.models;
using System;
using System.Collections.Generic;
using System.IO;

namespace NetDriveMonitor
{
	public class NDMCore
	{
		public IDatastore DataStore { get; set; }
		public HostnameWatchdog HostWatcher { get; set; }
		public SimpleNetDriveMapper Mapper { get; set; }

		private readonly string _saveFilePath = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}" +
												@"\Save\drives.json";

		public bool IsRunning { get; set; } // global app state

		public NDMCore()
		{
			DataStore = new DataStoreJson(_saveFilePath, true);
			HostWatcher = new HostnameWatchdog();
			Mapper = new SimpleNetDriveMapper();

			HostWatcher.OnHostChanged += Mapper.CheckDrive;
		}

		public bool Start()
		{
			if (!IsRunning)
			{
				// start app
				var netDrives = DataStore.Get(); // get data

				HostWatcher.Clear(); // clear host watcher
				Mapper.Clear(); // clear mapper drives

				// if drive list not empty
				if (netDrives?.Count > 0)
				{
					foreach (var drive in netDrives)
					{
						HostWatcher.AddHost(drive); // add hosts to host watcher
						Mapper.AddDrive(drive); // add drives to mapper
					}
				}

				HostWatcher.Start();

				return true;
			}
			else
			{
				HostWatcher.Stop();
				// app already running
				//DataStore.Save();
				return false;
			}
		}

		public bool Stop()
		{
			if (IsRunning)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}