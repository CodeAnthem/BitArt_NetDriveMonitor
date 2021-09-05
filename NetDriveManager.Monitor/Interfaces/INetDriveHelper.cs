using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetDriveManager.Monitor.Interfaces
{
	public interface INetDriveHelper
	{
		#region Public Methods

		bool Add(INetDrive drive);

		bool Add(string letter, string hostName, string share);

		Task<bool> AddAsync(INetDrive drive);

		IEnumerable<INetDrive> GetAll();

		bool IsAlreadyConnected(INetDrive drive);

		bool IsAlreadyConnected(string letter);

		bool Remove(INetDrive drive);

		bool Remove(string letter);

		bool RemoveAll();

		Task<bool> RemoveAsync(INetDrive drive);

		#endregion
	}
}