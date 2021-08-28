using System;
using System.Collections.Generic;

namespace NetDriveManager.Monitor.components.netDrivePingWatchdog
{
	public interface INetdrivePingWatchdog
	{
		bool IsRunning { get; }

		event Action<NetdriveMonitorModel> OnDriveAvailable;

		event Action<NetdriveMonitorModel> OnDriveUnavailable;

		bool Start(IEnumerable<NetdriveMonitorModel> drives);

		bool Stop();
	}
}