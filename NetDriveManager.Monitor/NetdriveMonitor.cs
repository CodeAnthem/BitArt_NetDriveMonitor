using NetDriveManager.Monitor.components.dataAccess;
using NetDriveManager.Monitor.components.netdriveHandler;
using NetDriveManager.Monitor.components.netDrivePingWatchdog;
using NetDriveManager.Monitor.components.netDrivePingWatchdog.parts;
using System.Collections.Generic;
using System.Diagnostics;

namespace NetDriveManager.Monitor
{
	public class NetdriveMonitor : INetdriveMonitor
	{
		public bool IsRunning { get; private set; }

		public List<NetdriveMonitorModel> Drives { get; private set; } = new List<NetdriveMonitorModel>();

		public readonly INetdriveMonitorSettings Settings;

		private readonly IDataAccess _dataAccess;

		private readonly INetdriveHandler _handler = new NetdriveHandler();

		private readonly NetdrivePingWatchdog _pingWatchdog = new NetdrivePingWatchdog();

		public NetdriveMonitor(INetdriveMonitorSettings netdriveMonitorSettings = null)
		{
			Settings = netdriveMonitorSettings ?? new DefaultSettings();
			var dataAccessor = new DataAccessJsonFile(Settings.JsonFile4Drives);
			_dataAccess = new DataAccess(dataAccessor, true);
			_pingWatchdog.OnHostStatusUpdated += HostStatusUpdated;
		}

		public bool Activate()
		{
			if (!IsRunning)
			{
				// Start
				Drives = GetDrives();

				IsRunning = true;
				return true;
			}
			Debug.WriteLine("Start aborted. Already running.");
			return false;
		}

		public bool Deactivate()
		{
			if (!IsRunning)
			{
				// Stop
				Drives.Clear();

				IsRunning = false;
				return true;
			}
			Debug.WriteLine("Stop aborted. Not running yet.");
			return false;
		}

		public bool ConnectDrive(NetdriveMonitorModel drive) => _handler.ConnectDrive(drive);

		public bool DisconnectDrive(NetdriveMonitorModel drive) => _handler.DisconnectDrive(drive);

		public List<NetdriveMonitorModel> GetDrives() => _dataAccess.GetDrives0REmptyList();

		public bool SaveDrives(List<NetdriveMonitorModel> drivesList) => _dataAccess.SaveDrives(drivesList);

		public bool StartPingMonitor() => _pingWatchdog.Start(Drives);

		public bool StopPingMonitor() => _pingWatchdog.Stop();

		private void HostStatusUpdated(NetdrivePingWatchdogReportModel report)
		{
			if (report.IsOnline)
			{
				ConnectDrive(report.Drive);
			}
			DisconnectDrive(report.Drive);
		}
	}
}