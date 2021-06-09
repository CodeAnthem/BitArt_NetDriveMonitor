using NetDriveMonitor.interfaces;
using System;
using System.Collections.Generic;

namespace NetDriveMonitor.components
{
	public class DataStoreJson : IDatastore
	{
		public List<INetDrive> Get()
		{
			throw new NotImplementedException();
		}

		public bool Save(List<INetDrive> netDrives)
		{
			throw new NotImplementedException();
		}
	}
}