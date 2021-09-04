using NetDriveManager.Monitor.Models;

namespace NetDriveManager.Monitor.Interfaces
{
	public interface INetDriveFactory
	{
		#region Public Methods

		INetDrive Create(NetDriveInfo info, NetDriveOptions options, NetDriveStatus status);

		NetDriveModel Create(string letter, string hostName, string share, bool acIfAvailable = false, bool acIfAccessed = false, bool acLanOnly = false);

		NetDriveInfo CreateDriveInfo(string letter, string hostName, string share);

		#endregion
	}
}