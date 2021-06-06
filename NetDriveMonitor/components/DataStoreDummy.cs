using NetDriveMonitor.interfaces;
using NetDriveMonitor.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveMonitor.components
{
	public class DataStoreDummy : IDatastore
	{
		public List<NetDriveModel> Get()
		{
			var driveList = new List<NetDriveModel>();

			driveList.Add(new NetDriveModel("H:", "dp-nas10", "10.8.0.1", "home"));
			driveList.Add(new NetDriveModel("M:", "dp-nas10", "10.8.0.1", "music"));
			driveList.Add(new NetDriveModel("V:", "dp-nas10", "10.8.0.1", "video"));

			return driveList;
		}

		public bool Save(List<NetDriveModel> drives)
		{
			return true;
		}
	}
}