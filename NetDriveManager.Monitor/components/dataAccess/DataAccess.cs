using System.Collections.Generic;

namespace NetDriveManager.Monitor.components.dataAccess
{
	public class DataAccess : IDataAccess
	{
		#region Private Fields

		private readonly bool _useDummyDataIfEmpty;
		private readonly IDataAccess _da;
		private static readonly List<NetdriveMonitorModel> _dummyNetdriveList = new List<NetdriveMonitorModel>
			{
				new NetdriveMonitorModel("H:", "dp-nas10", "home", true),
				new NetdriveMonitorModel("V:", "dp-nas10", "video")
			};

		#endregion Private Fields

		#region Public Constructors

		public DataAccess(IDataAccess da, bool useDummyDataIfEmpty)
		{
			_da = da;
			_useDummyDataIfEmpty = useDummyDataIfEmpty;
		}

		#endregion Public Constructors

		#region Public Methods

		public List<NetdriveMonitorModel> GetDrives0REmptyList()
		{
			List<NetdriveMonitorModel> netdrivesList = _da.GetDrives0REmptyList();
			return PassDummyDataIfRequired(netdrivesList);
		}

		public bool SaveDrives(List<NetdriveMonitorModel> netDrives) => _da.SaveDrives(netDrives);

		#endregion Public Methods

		#region Private Methods

		private List<NetdriveMonitorModel> PassDummyDataIfRequired(List<NetdriveMonitorModel> netdrivesList)
		{
			bool drivesFound = netdrivesList.Count > 0;
			if (!drivesFound && _useDummyDataIfEmpty)
			{
				return _dummyNetdriveList;
			}
			return netdrivesList;
		}

		#endregion Private Methods
	}
}