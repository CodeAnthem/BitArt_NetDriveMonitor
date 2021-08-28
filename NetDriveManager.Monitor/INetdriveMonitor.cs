using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NetDriveManager.Monitor
{
	public interface INetdriveMonitor
	{
		ObservableCollection<NetdriveMonitorModel> Drives { get; }
		bool IsEnabled { get; }

		bool Activate();

		bool ConnectDrive(NetdriveMonitorModel drive);

		bool Deactivate();

		bool DisconnectDrive(NetdriveMonitorModel drive);

		IEnumerable<NetdriveMonitorModel> GetDrives();

		bool SaveDrives(List<NetdriveMonitorModel> drivesList);

		bool StartPingMonitor();

		bool StopPingMonitor();
	}
}