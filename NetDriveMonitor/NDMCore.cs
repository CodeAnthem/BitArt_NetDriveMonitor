using NetDriveMonitor.components;
using NetDriveMonitor.interfaces;
using System;

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