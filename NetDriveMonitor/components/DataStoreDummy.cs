using NetDriveMonitor.interfaces;
using NetDriveMonitor.models;
using System;
using System.Collections.Generic;

namespace NetDriveMonitor.components
{
	public class DataStoreDummy : IDatastore
	{
		public bool Save(List<INetDrive> netDrives)
		{
			throw new NotImplementedException();
		}

		public List<INetDrive> Get()
		{
			var driveList = new List<INetDrive>
			{
				new NetDriveModel("H:", "dp-nas10", "home"),
				new NetDriveModel("M:", "dp-nas10", "music"),
				new NetDriveModel("V:", "dp-nas10", "video")
			};

			return driveList;
		}
	}
}