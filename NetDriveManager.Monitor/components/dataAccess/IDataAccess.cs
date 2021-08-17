using System.Collections.Generic;

namespace NetDriveManager.Monitor.components.dataAccess
{
	public interface IDataAccess
	{
		bool UseDummyDataIfEmpty { get; set; }

		List<NetdriveMonitorModel> GetDrives0REmptyList();
		List<NetdriveMonitorModel> GetDummyData();
		bool SaveDrives(List<NetdriveMonitorModel> netDrives);
	}
}