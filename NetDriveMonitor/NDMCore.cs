using NetDriveMonitor.components;
using NetDriveMonitor.interfaces;
using System;
using System.IO;

namespace NetDriveMonitor
{
	public class NDMCore
	{
		public IDatastore DataStore { get; set; }
		public WatchdogMonitor Monitor { get; set; }

		private string _saveFilePath = @$"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\Save\drives.json";

		public NDMCore()
		{
			DataStore = new DataStoreJson(_saveFilePath);
			Monitor = new WatchdogMonitor();
		}
	}
}