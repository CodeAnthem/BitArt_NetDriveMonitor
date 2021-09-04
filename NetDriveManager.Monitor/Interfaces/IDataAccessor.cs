using System.Collections.Generic;

namespace NetDriveManager.Monitor.Interfaces
{
	public interface IDataAccessor
	{
		#region Public Methods

		List<INetDrive> GetDrives0RNull();

		bool SaveDrives(List<INetDrive> netDrives);

		#endregion
	}
}