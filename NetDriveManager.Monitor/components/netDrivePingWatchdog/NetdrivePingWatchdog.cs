using BitArt_Network_Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NetDriveManager.Monitor.components.netDrivePingWatchdog
{
	public class NetdrivePingWatchdog : INetdrivePingWatchdog
	{
		public event Action<NetdriveMonitorModel> OnDriveAvailable;

		public event Action<NetdriveMonitorModel> OnDriveUnavailable;

		public bool IsRunning { get; private set; }

		private readonly INetdrivePingWatchdogStore _store;

		private readonly IHostMonitor _pingMonitor;

		public NetdrivePingWatchdog(INetdrivePingWatchdogStore netdrivePingWatchdogStore, IHostMonitor hostMonitor)
		{
			_store = netdrivePingWatchdogStore;
			_pingMonitor = hostMonitor;

			_pingMonitor.ScanInterval = 1000;
			_pingMonitor.NotifyOnChangesOnly = true;
			_pingMonitor.NotifyOnFirstPing = false;
			_pingMonitor.OnHostChanged += HostUpdated;
		}

		public bool Start(List<NetdriveMonitorModel> drives)
		{
			if (!IsRunning)
			{
				if (drives.Count > 0)
				{
					drives.ForEach(x => _store.AddDriveIfRequired(x));
					_store.GetHosts().ForEach(x => _pingMonitor.AddHost(x));
					_pingMonitor.Start();

					IsRunning = true;
					return true;
				}
				Debug.WriteLine("Start aborted. Drives empty");
				return false;
			}
			Debug.WriteLine("Start aborted. Already running.");
			return false;
		}

		public bool Stop()
		{
			if (!IsRunning)
			{
				_pingMonitor.Stop();
				_pingMonitor.Clear();

				_store.ClearList();
				IsRunning = false;
				return true;
			}
			Debug.WriteLine("Stop aborted. Not running yet.");
			return false;
		}

		private void HostUpdated(string hostName, bool state)
		{
			var affectedDrives = _store.GetDrivesOfHost(hostName);
			foreach (var drive in affectedDrives)
			{
				if (state)
				{
					OnDriveAvailable?.Invoke(drive);
				}
				else
				{
					OnDriveUnavailable?.Invoke(drive);
				}
			}
		}
	}
}