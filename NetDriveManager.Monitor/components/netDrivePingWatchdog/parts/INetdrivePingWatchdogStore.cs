using System.Collections.Generic;

namespace NetDriveManager.Monitor.components.netDrivePingWatchdog.parts
{
	public interface INetdrivePingWatchdogStore
	{
		void AddDriveIfRequired(NetdriveMonitorModel drive);
		void ClearList();
		List<NetdriveMonitorModel> GetDrivesOfHost(string hostnameOrIP);
		List<string> GetHosts();
	}
}