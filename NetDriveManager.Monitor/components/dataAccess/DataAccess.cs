using NetDriveManager.Monitor.Interfaces;
using NetDriveManager.Monitor.Models;
using Serilog;
using System;
using System.Collections.Generic;

namespace NetDriveManager.Monitor.components.dataAccess
{
	public class DataAccess : IDataAccess
	{
		#region Private Fields

		private readonly IDataAccessor _accessor;
		private readonly INetDriveFactory _factory;

		#endregion

		#region Public Constructors

		public DataAccess(IDataAccessor dataAccessor, INetDriveFactory factory)
		{
			_accessor = dataAccessor;
			_factory = factory;
		}

		#endregion

		#region Public Properties

		public bool UseDummyDataIfEmpty { get; set; }

		#endregion

		#region Public Methods

		public List<INetDrive> GetDrives0REmptyList()
		{
			List<INetDrive> netdrivesList = _accessor.GetDrives0RNull();
			netdrivesList ??= new List<INetDrive>();

			if (netdrivesList.Count == 0 && UseDummyDataIfEmpty)
			{
				Log.Debug("Loaded dummy drives");

				return GetDummyData();
			}
			Log.Information("Loaded drives");
			return netdrivesList;
		}

		public List<INetDrive> GetDummyData()
		{
			List<INetDrive> dummyDrives = new List<INetDrive>();
			dummyDrives.Add(_factory.Create("H:", "dp-nas10", "home"));
			dummyDrives.Add(_factory.Create("L:", "dp-nas10", "soft"));
			dummyDrives.Add(_factory.Create("V:", "dp-nas10", "video"));
			return dummyDrives;
		}

		public bool SaveDrives(List<INetDrive> netDrives) => _accessor.SaveDrives(netDrives);

		#endregion
	}
}