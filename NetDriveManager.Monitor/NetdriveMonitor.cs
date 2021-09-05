using BitArt_Network_Helpers;
using NetDriveManager.Monitor.components.NetDriveWatcher;
using NetDriveManager.Monitor.Interfaces;
using Serilog;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace NetDriveManager.Monitor
{
	public class NetDriveMonitor : INetDriveMonitor
	{
		#region Private Fields

		private readonly NetDriveWatcherTimer _driveWatcher;
		private readonly INetDriveHelper _helper;
		private readonly IHostMonitor _hostMonitor;
		private readonly ILogger _logger;
		private readonly INetDriveStore _store;

		#endregion

		#region Public Properties

		public IDataAccess DataAccess { get; }
		public INetDriveHelper DriveHelper { get; }
		public ObservableCollection<INetDrive> Drives { get => _store.Drives; }
		public bool IsEnabled { get; private set; }
		public INetDriveMonitorSettings NetDriveMonitorSettings { get; }

		#endregion

		#region Public Constructors

		public NetDriveMonitor(ILogger logger, IDataAccess dataAccess, INetDriveStore store, IHostMonitor hostMonitor, NetDriveWatcherTimer driveWatcher, INetDriveHelper driveHelper, INetDriveMonitorSettings netDriveMonitorSettings)
		{
			_logger = logger.ForContext<NetDriveMonitor>();
			DataAccess = dataAccess;
			DataAccess.UseDummyDataIfEmpty = true;
			_store = store;
			_hostMonitor = hostMonitor;

			_hostMonitor.ScanInterval = 1000;
			_hostMonitor.NotifyOnChangesOnly = true;
			_hostMonitor.NotifyOnFirstPing = false;

			_hostMonitor.OnHostChanged += HostStatusChanged;
			_driveWatcher = driveWatcher;
			DriveHelper = driveHelper;
			NetDriveMonitorSettings = netDriveMonitorSettings;

			//_pingWatchdog.OnDriveAvailable += x => ConnectDrive(x);
			//_pingWatchdog.OnDriveUnavailable += x => DisconnectDrive(x);
		}

		private void HostStatusChanged(string hostName, bool state)
		{
			var drivesOfHost = _store.GetDrivesOfHostOREMPTY(hostName);

			foreach (var drive in drivesOfHost)
			{
				drive.Status.IsHostAvailable = state;
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
					if (drive.Options.AutoConnectIfAvailable && !drive.Status.IsConnected && NetDriveMonitorSettings.IsAutoConnectIfAvailable) // TODO: Add check if auto connect option is enabled
						DriveHelper.Add(drive);
				}
				else
				{
					if (drive.Status.IsConnected)
						DriveHelper.Remove(drive);
				}
			}
			Log.Debug("Updated all drives of host: {host}", hostName);
		}

		#endregion

		#region Public Methods

		public bool Activate()
		{
			if (!IsEnabled)
			{
				_store.Clear();

				// Get drives and register them into store
				var drivesOfConfigFile = DataAccess.GetDrives0REmptyList();
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
				_driveWatcher.Start();

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

		#endregion
	}
}