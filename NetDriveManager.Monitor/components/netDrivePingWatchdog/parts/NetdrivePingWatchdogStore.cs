using System.Collections.Generic;
using System.Diagnostics;

namespace NetDriveManager.Monitor.components.netDrivePingWatchdog.parts
{
	public class NetdrivePingWatchdogStore
	{
		private readonly Dictionary<string, List<NetdriveMonitorModel>> _activeDrivesList = new Dictionary<string, List<NetdriveMonitorModel>>();

		public void ClearList() => _activeDrivesList.Clear();

		public List<string> GetHosts()
		{
			List<string> hosts = new List<string>();
			foreach (var key in _activeDrivesList.Keys)
			{
				hosts.Add(key);
			}
			return hosts;
		}

		public List<NetdriveMonitorModel> GetDrivesOfHost(string hostnameOrIP) => _activeDrivesList[hostnameOrIP];

		public void AddDriveIfRequired(NetdriveMonitorModel drive)
		{
			if (drive.AutoConnectIfAvailable)
			{
				string hostnameOrIP = drive.HostName;
				AddHostIfNotExistYet(hostnameOrIP);
				_activeDrivesList[hostnameOrIP].Add(drive);
			}
		}

		private void AddHostIfNotExistYet(string hostnameOrIP)
		{
			bool isHostAlreadyKnown = _activeDrivesList.ContainsKey(hostnameOrIP);
			if (!isHostAlreadyKnown)
			{
				_activeDrivesList.Add(hostnameOrIP, new List<NetdriveMonitorModel>());
				Debug.WriteLine($"Ping Watchdog: Added host: {hostnameOrIP}");
			}
		}
	}
}