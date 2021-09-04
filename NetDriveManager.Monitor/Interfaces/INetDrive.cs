using NetDriveManager.Monitor.Models;

namespace NetDriveManager.Monitor.Interfaces
{
	public interface INetDrive
	{
		#region Public Properties

		NetDriveInfo Info { get; set; }
		NetDriveOptions Options { get; set; }
		NetDriveStatus Status { get; set; }

		#endregion

		#region Public Methods

		bool Equals(object o);

		int GetHashCode();

		string ToString();

		#endregion
	}
}