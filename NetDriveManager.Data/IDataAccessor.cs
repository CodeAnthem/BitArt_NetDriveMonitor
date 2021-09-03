using System.Collections.Generic;

namespace NetDriveManager.Data
{
	public interface IDataAccessor
	{
		#region Public Methods

		List<NetdriveMonitorModel> GetDrives0RNull();

		bool SaveDrives(List<NetdriveMonitorModel> netDrives);

		#endregion
	}
}