using NetDriveMonitor.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveMonitor.interfaces
{
	public interface IDataAccess
	{
		public IEnumerable<NetDriveMonitorModel> GetDrives();

		public bool SaveDrives(IEnumerable<NetDriveMonitorModel> netDrives);
	}
}