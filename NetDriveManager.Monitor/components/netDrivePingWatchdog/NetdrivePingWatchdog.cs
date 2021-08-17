using BitArt_Network_Helpers;
using NetDriveManager.Monitor.components.netDrivePingWatchdog.parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NetDriveManager.Monitor.components.netDrivePingWatchdog
{
	public class NetdrivePingWatchdog
	{
		public event Action<NetdrivePingWatchdogReportModel> OnHostStatusUpdated;

		public bool IsRunning { get; private set; }

		private readonly NetdrivePingWatchdogStore _store;

		private readonly HostMonitor _pingMonitor;

		public NetdrivePingWatchdog()
		{
			_store = new NetdrivePingWatchdogStore();

			_pingMonitor = new HostMonitor();
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
				var report = new NetdrivePingWatchdogReportModel(drive, state);
				OnHostStatusUpdated?.Invoke(report);
			}
		}
	}
}