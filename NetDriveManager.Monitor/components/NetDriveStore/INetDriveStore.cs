using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NetDriveManager.Monitor.components.NetDriveStore
{
	public interface INetDriveStore
	{
		ObservableCollection<NetdriveMonitorModel> Drives { get; }
		Dictionary<string, List<NetdriveMonitorModel>> DrivesByHostDict { get; set; }

		void Clear();
		List<NetdriveMonitorModel> GetDrivesOfHostOREMPTY(string hostnameOrIP);
		List<NetdriveMonitorModel> GetDrivesOfHostOREMPTY2(string hostnameOrIP);
		List<string> GetRegisteredHostsOREMPTY();
		void Register(NetdriveMonitorModel drive);
	}
}