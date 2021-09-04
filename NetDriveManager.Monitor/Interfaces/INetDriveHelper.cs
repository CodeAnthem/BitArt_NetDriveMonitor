using System.Collections.Generic;

namespace NetDriveManager.Monitor.Interfaces
{
	public interface INetDriveHelper
	{
		#region Public Methods

		bool Add(INetDrive drive);

		bool Add(string letter, string hostName, string share);

		IEnumerable<INetDrive> GetAll();

		bool IsAlreadyConnected(INetDrive drive);

		bool IsAlreadyConnected(string letter);

		bool Remove(INetDrive drive);

		bool Remove(string letter);

		bool RemoveAll();

		#endregion
	}
}