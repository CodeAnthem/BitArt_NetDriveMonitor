using NetDriveMonitor.helpers;
using NetDriveMonitor.interfaces;
using NetDriveMonitor.components;
using System;
using System.Collections.Generic;
using System.Text;
using NetDriveMonitor.models;

namespace NetDriveMonitor
{
	public class NDMCore
	{
		public IDatastore DataStore { get; set; }
		public WatchdogMonitor Monitor { get; set; }

		public NDMCore()
		{
			DataStore = new DataStoreDummy();
			Monitor = new WatchdogMonitor();
		}

		public void Start()
		{
			// get data of datastore (json or dummy data)
			var drives = DataStore.Get();
			Console.WriteLine("Drives found: " + drives.Count);

			Monitor.StartMonitoring(drives);
		}

		public void Stop()
		{
		}
	}
}