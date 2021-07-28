using NetDriveMonitor.models;
using System.Collections.Generic;

namespace NetDriveMonitor.interfaces
{
	public interface IDatastore
	{
		public List<NetDriveMonitorModel> Get();

		public bool Save(List<NetDriveMonitorModel> netDrives);
	}
}