using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace NetDriveManager.Monitor.Interfaces
{
	public interface INetDriveMonitor
	{
		#region Public Properties

		IDataAccess DataAccess { get; }
		INetDriveHelper DriveHelper { get; }
		ObservableCollection<INetDrive> Drives { get; }
		bool IsEnabled { get; }
		INetDriveMonitorSettings NetDriveMonitorSettings { get; }

		#endregion

		#region Public Methods

		bool Activate();

		bool Deactivate();

		#endregion
	}
}