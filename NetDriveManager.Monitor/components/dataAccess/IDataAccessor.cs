using System.Collections.Generic;

namespace NetDriveManager.Monitor.components.dataAccess
{
	public interface IDataAccessor
	{
		List<NetdriveMonitorModel> GetDrives0RNull();

		bool SaveDrives(List<NetdriveMonitorModel> netDrives);
	}
}