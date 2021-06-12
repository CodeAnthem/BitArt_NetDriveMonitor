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

		public NDMCore()
		{
			DataStore = new DataStoreJson(_saveFilePath, true);
			HostWatcher = new HostnameWatchdog();
			Mapper = new SimpleNetDriveMapper();

			HostWatcher.OnHostChanged += Mapper.CheckDrive;
		}

		public void ReadDrives(List<INetDrive> driveList)
		{
			if (driveList?.Count > 0)
			{
				foreach (var drive in driveList)
				{
					HostWatcher.Clear();
					HostWatcher.AddHost(drive); // add hosts to host watcher
					Mapper.Clear();
					Mapper.AddDrive(drive); // add drives to mapper
				}
			}
		}
	}
}