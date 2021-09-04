using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NetDriveManager.Monitor.Interfaces
{
	public interface INetDriveMonitor
	{
		#region Public Properties

		ObservableCollection<INetDrive> Drives { get; }
		bool IsEnabled { get; }

		#endregion

		#region Public Methods

		bool Activate();

		bool ConnectDrive(INetDrive drive);

		bool Deactivate();

		bool DisconnectDrive(INetDrive drive);

		IEnumerable<INetDrive> GetDrives();

		bool SaveDrives(List<INetDrive> drivesList);

		#endregion
	}
}