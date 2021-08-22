using Serilog;
using System;
using System.Collections.Generic;

namespace NetDriveManager.Monitor.components.dataAccess
{
	public class DataAccess : IDataAccess
	{
		public bool UseDummyDataIfEmpty { get; set; }

		#region Private Fields

		private static readonly List<NetdriveMonitorModel> _dummyNetdriveList = new List<NetdriveMonitorModel>
			{
				new NetdriveMonitorModel("H:", "dp-nas10", "home", true),
				new NetdriveMonitorModel("V:", "dp-nas10", "video")
			};

		private readonly IDataAccessor _accessor;

		#endregion Private Fields

		#region Public Constructors

		public DataAccess(IDataAccessor dataAccessor)
		{
			_accessor = dataAccessor;
		}

		#endregion Public Constructors

		#region Public Methods

		public List<NetdriveMonitorModel> GetDrives0REmptyList()
		{
			List<NetdriveMonitorModel> netdrivesList = _accessor.GetDrives0RNull();
			netdrivesList ??= new List<NetdriveMonitorModel>();

			if (netdrivesList.Count == 0 && UseDummyDataIfEmpty)
			{
				Log.Debug("Loaded dummy drives");

				return _dummyNetdriveList;
			}
			Log.Information("Loaded drives");
			return netdrivesList;
		}

		public List<NetdriveMonitorModel> GetDummyData() => _dummyNetdriveList;

		public bool SaveDrives(List<NetdriveMonitorModel> netDrives) => _accessor.SaveDrives(netDrives);

		#endregion Public Methods
	}
}