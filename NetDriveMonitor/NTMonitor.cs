using NetDriveMonitor.helpers;
using NetDriveMonitor.interfaces;
using NetDriveMonitor.components;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveMonitor
{
	public class NTMonitor
	{
		public IDatastore DataStore { get; set; }
		public NetDriveHelper NDHelper { get; set; }

		public NTMonitor()
		{
			NDHelper = new NetDriveHelper();
			DataStore = new DataStoreDummy();
		}
	}
}