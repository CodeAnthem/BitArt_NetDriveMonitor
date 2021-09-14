using NetDriveManager.Monitor.Models;

namespace NetDriveManager.Monitor.Interfaces
{
	public interface INetDriveFactory
	{
		#region Public Methods

		INetDrive Create(NetDriveInfo info, NetDriveOptions options, NetDriveStatus status);

		INetDrive Create(string letter, string hostName, string share, bool acIfAvailable = false, bool acLanOnly = false);

		INetDrive Create(NetDriveInfo info);

		NetDriveInfo CreateDriveInfo(string letter, string hostName, string share);

		#endregion
	}
}