using System.Collections.Generic;

namespace NetDriveManager.Data
{
	public interface IDataAccess
	{
		#region Public Properties

		bool UseDummyDataIfEmpty { get; set; }

		#endregion

		#region Public Methods

		List<NetdriveMonitorModel> GetDrives0REmptyList();

		List<NetdriveMonitorModel> GetDummyData();

		bool SaveDrives(List<NetdriveMonitorModel> netDrives);

		#endregion
	}
}