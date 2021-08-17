using System.Collections.Generic;

namespace NetDriveManager.Monitor.components.dataAccess
{
	public interface IDataAccess
	{
		List<NetdriveMonitorModel> GetDrives0REmptyList();

		bool SaveDrives(List<NetdriveMonitorModel> netDrives);
	}
}