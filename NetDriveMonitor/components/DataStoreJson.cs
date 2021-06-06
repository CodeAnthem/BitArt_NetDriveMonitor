using NetDriveMonitor.interfaces;
using NetDriveMonitor.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveMonitor.components
{
	public class DataStoreJson : IDatastore
	{
		public List<NetDriveModel> Get()
		{
			throw new NotImplementedException();
		}

		public bool Save(List<NetDriveModel> drives)
		{
			throw new NotImplementedException();
		}
	}
}