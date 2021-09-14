using System.Collections.Generic;

namespace NetDriveManager.Monitor.Interfaces
{
	public interface INetDriveMounter<T>
	{
		#region Public Methods

		bool Add(INetDrive drive);

		IEnumerable<INetDrive> GetAll();

		bool Remove(string letter);

		bool Remove(char letter);

		bool RemoveAll();

		#endregion
	}
}