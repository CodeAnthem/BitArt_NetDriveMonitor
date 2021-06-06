using NetDriveMonitor.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveMonitor.interfaces
{
	public interface IDatastore
	{
		public List<NetDriveModel> Get();

		public bool Save(List<NetDriveModel> drives);
	}
}