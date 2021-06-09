using System.Collections.Generic;

namespace NetDriveMonitor.interfaces
{
	public interface IDatastore
	{
		public List<INetDrive> Get();

		public bool Save(List<INetDrive> netDrives);
	}
}