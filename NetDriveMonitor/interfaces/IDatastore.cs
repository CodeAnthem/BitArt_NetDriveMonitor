using NetDriveMonitor.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveMonitor.interfaces
{
	public interface IDatastore
	{
		public List<INetDrive> Get();

		public bool Save(List<INetDrive> netDrives);
	}
}