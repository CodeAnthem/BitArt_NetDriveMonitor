using BitArt_Network_Helpers;
using NetDriveManager.Monitor.components.dataAccess;
using NetDriveManager.Monitor.components.netdriveHandler;
using NetDriveManager.Monitor.components.NetDriveStore;
using Serilog;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NetDriveManager.Monitor
{
	public class NetdriveMonitor : INetdriveMonitor
	{
		#region Private Fields

		private readonly IDataAccess _da;
		private readonly INetdriveHandler _handler;
		private readonly IHostMonitor _hostMonitor;
		private readonly ILogger _logger;
		private readonly INetDriveStore _store;

		#endregion

		#region Public Properties

		public ObservableCollection<NetdriveMonitorModel> Drives { get => _store.Drives; }

		public bool IsEnabled { get; private set; }

		#endregion

		#region Public Constructors

		private void HostStatusChanged(string hostName, bool state)
		{
			var drivesOfHost = _store.GetDrivesOfHostOREMPTY(hostName);

			foreach (var drive in drivesOfHost)
			{
				drive.IsHostAvailable = state;
				if (state)
				{
					Log.Debug("Changed host status of drive {drive} to online", drive);
				}
				else
				{
					Log.Debug("Changed host status of drive {drive} to offline", drive);
				}

				if (state)
				{
					if (drive.AutoConnectIfAvailable)
						ConnectDrive(drive);
				}
				else
				{
					DisconnectDrive(drive);
				}
			}
			Log.Debug("Updated all drives of host: {host}", hostName);
		}

		public NetdriveMonitor(ILogger logger, IDataAccess dataAccess, INetdriveHandler netdriveHandler, INetDriveStore store, IHostMonitor hostMonitor)
		{
			_logger = logger.ForContext<NetdriveMonitor>();
			_da = dataAccess;
			_da.UseDummyDataIfEmpty = true;
			_store = store;
			_handler = netdriveHandler;
			_hostMonitor = hostMonitor;

			_hostMonitor.ScanInterval = 1000;
			_hostMonitor.NotifyOnChangesOnly = true;
			_hostMonitor.NotifyOnFirstPing = false;

			_hostMonitor.OnHostChanged += HostStatusChanged;

			//_pingWatchdog.OnDriveAvailable += x => ConnectDrive(x);
			//_pingWatchdog.OnDriveUnavailable += x => DisconnectDrive(x);
		}

		#endregion

		#region Public Methods

		public bool Activate()
		{
			if (!IsEnabled)
			{
				_store.Clear();

				// Get drives and register them into store
				var drivesOfConfigFile = GetDrives();
				foreach (var drive in drivesOfConfigFile)
				{
					_store.Register(drive);
				}

				var listOfHosts = _store.GetRegisteredHostsOREMPTY();
				foreach (var hostOrIP in listOfHosts)
				{
					_hostMonitor.AddHost(hostOrIP);
					Log.Debug("Host {host} added to ping monitor", hostOrIP);
				}
				Log.Debug("Added {count} hosts to ping watch", _hostMonitor.HostCount);

				// Start
				_hostMonitor.Start();

				//TODO: Check drives (are they online, offline OR bla)
				//foreach (var drive in Drives)
				//{
				//	_handler.
				//}

				IsEnabled = true;
				return true;
			}
			Log.Warning("Start aborted, already running");
			return false;
		}

		public bool ConnectDrive(NetdriveMonitorModel drive) => _handler.ConnectDrive(drive);

		public bool Deactivate()
		{
			if (!IsEnabled)
			{
				// Stop
				_hostMonitor.Stop();

				IsEnabled = false;
				return true;
			}
			Log.Warning("Stop aborted, not running yet");
			return false;
		}

		public bool DisconnectDrive(NetdriveMonitorModel drive) => _handler.DisconnectDrive(drive);

		public IEnumerable<NetdriveMonitorModel> GetDrives() => _da.GetDrives0REmptyList();

		public bool SaveDrives(List<NetdriveMonitorModel> drivesList) => _da.SaveDrives(drivesList);

		#endregion
	}
}