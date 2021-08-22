using NetDriveManager.Monitor.components.dataAccess;
using NetDriveManager.Monitor.components.netdriveHandler;
using NetDriveManager.Monitor.components.netDrivePingWatchdog;
using Serilog;
using System.Collections.Generic;

namespace NetDriveManager.Monitor
{
	public class NetdriveMonitor : INetdriveMonitor
	{
		public bool IsEnabled { get; private set; }

		public List<NetdriveMonitorModel> Drives { get; private set; } = new List<NetdriveMonitorModel>();

		private readonly IDataAccess _da;

		private readonly INetdriveHandler _handler;

		private readonly INetdrivePingWatchdog _pingWatchdog;
		private readonly ILogger _logger;

		public NetdriveMonitor(ILogger logger, IDataAccess dataAccess, INetdriveHandler netdriveHandler, INetdrivePingWatchdog netdrivePingWatchdog)
		{
			_logger = logger.ForContext<NetdriveMonitor>();
			_da = dataAccess;
			_da.UseDummyDataIfEmpty = true;
			_handler = netdriveHandler;
			_pingWatchdog = netdrivePingWatchdog;

			_pingWatchdog.OnDriveAvailable += x => ConnectDrive(x);
			_pingWatchdog.OnDriveUnavailable += x => DisconnectDrive(x);
		}

		public bool Activate()
		{
			if (!IsEnabled)
			{
				// Start
				Drives = GetDrives();

				IsEnabled = true;
				return true;
			}
			Log.Warning("Start aborted, already running");
			return false;
		}

		public bool Deactivate()
		{
			if (!IsEnabled)
			{
				// Stop
				Drives.Clear();

				IsEnabled = false;
				return true;
			}
			Log.Warning("Stop aborted, not running yet");
			return false;
		}

		public bool ConnectDrive(NetdriveMonitorModel drive) => _handler.ConnectDrive(drive);

		public bool DisconnectDrive(NetdriveMonitorModel drive) => _handler.DisconnectDrive(drive);

		public List<NetdriveMonitorModel> GetDrives() => _da.GetDrives0REmptyList();

		public bool SaveDrives(List<NetdriveMonitorModel> drivesList) => _da.SaveDrives(drivesList);

		public bool StartPingMonitor() => _pingWatchdog.Start(Drives);

		public bool StopPingMonitor() => _pingWatchdog.Stop();
	}
}