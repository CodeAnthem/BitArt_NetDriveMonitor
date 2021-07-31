using NetDriveMonitor.interfaces;
using NetDriveMonitor.models;
using System.Collections.Generic;
using System.Linq;

namespace NetDriveMonitor.components
{
	public class DataAccess : IDataAccess
	{
		private readonly bool _useDummyDataIfEmpty;
		private readonly IDataAccess _da;

		public DataAccess(IDataAccess da, bool useDummyDataIfEmpty)
		{
			_da = da;
			_useDummyDataIfEmpty = useDummyDataIfEmpty;
		}

		public IEnumerable<NetDriveMonitorModel> GetDrives()
		{
			var drives = _da.GetDrives();
			var drivesFound = !drives.Any<NetDriveMonitorModel>();
			if (!drivesFound && _useDummyDataIfEmpty)
			{
				return CreateDummyData();
			}
			return drives;
		}

		public bool SaveDrives(IEnumerable<NetDriveMonitorModel> netDrives) => _da.SaveDrives(netDrives);

		private IEnumerable<NetDriveMonitorModel> CreateDummyData()
		{
			return new List<NetDriveMonitorModel>
			{
				new NetDriveMonitorModel("H:", "dp-nas10", "home", true),
				new NetDriveMonitorModel("V:", "dp-nas10", "video")
			};
		}
	}
}