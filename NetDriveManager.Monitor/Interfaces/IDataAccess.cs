using System.Collections.Generic;

namespace NetDriveManager.Monitor.Interfaces
{
	public interface IDataAccess
	{
		#region Public Properties

		bool UseDummyDataIfEmpty { get; set; }

		#endregion

		#region Public Methods

		List<INetDrive> GetDrives0REmptyList();

		List<INetDrive> GetDummyData();

		bool SaveDrives(List<INetDrive> netDrives);

		#endregion
	}
}