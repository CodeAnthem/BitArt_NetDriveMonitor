using System.Collections.Generic;

namespace NetDriveManager.Monitor
{
	public interface INetdriveMonitor
	{
		List<NetdriveMonitorModel> Drives { get; }
		bool IsEnabled { get; }

		bool Activate();
		bool ConnectDrive(NetdriveMonitorModel drive);
		bool Deactivate();
		bool DisconnectDrive(NetdriveMonitorModel drive);
		List<NetdriveMonitorModel> GetDrives();
		bool SaveDrives(List<NetdriveMonitorModel> drivesList);
		bool StartPingMonitor();
		bool StopPingMonitor();
	}
}