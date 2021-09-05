using NetDriveManager.Monitor.components.Watchers;
using NetDriveManager.Monitor.Interfaces;
using Serilog;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace NetDriveManager.Monitor
{
	public class NetDriveMonitor : INetDriveMonitor
	{
		#region Private Fields

		private readonly NetDriveWatcher _driveWatcher;
		private readonly INetDriveHelper _helper;
		private readonly IHostWatcher _hostWatcher;
		private readonly ILogger _logger;
		private readonly INetDriveStore _store;
		private readonly System.Timers.Timer _timer = new System.Timers.Timer();

		#endregion

		#region Public Properties

		public IDataAccess DataAccess { get; }
		public INetDriveHelper DriveHelper { get; }
		public ObservableCollection<INetDrive> Drives { get => _store.Drives; }
		public bool IsEnabled { get; private set; }
		public INetDriveMonitorSettings Settings { get; }
		private bool isScanDoneOnce;

		#endregion

		#region Public Constructors

		public NetDriveMonitor(ILogger logger, IDataAccess dataAccess, INetDriveStore store, NetDriveWatcher driveWatcher, INetDriveHelper driveHelper, INetDriveMonitorSettings netDriveMonitorSettings, IHostWatcher hostWatcher)
		{
			//_logger = logger.ForContext<NetDriveMonitor>();
			_logger = logger;

			DataAccess = dataAccess;
			DataAccess.UseDummyDataIfEmpty = true;
			_store = store;

			_hostWatcher = hostWatcher;
			_hostWatcher.NotifyOnChangesOnly = true;
			_hostWatcher.NotifyOnFirstPing = true;
			_hostWatcher.OnHostChanged += HostStatusChanged;

			_driveWatcher = driveWatcher;
			DriveHelper = driveHelper;
			Settings = netDriveMonitorSettings;

			TimerSetup();
			_hostWatcher = hostWatcher;
			//_pingWatchdog.OnDriveAvailable += x => ConnectDrive(x);
			//_pingWatchdog.OnDriveUnavailable += x => DisconnectDrive(x);
		}

		private void TimerSetup()
		{
			_timer.Interval = 1000;
			_timer.AutoReset = true;
			_timer.Enabled = false;
			_timer.Elapsed += Timer_Elapsed;
		}

		private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			_timer.Enabled = false;
			await _hostWatcher.ScanAllHostsAsyncParallel();
			//await Task.Delay(2000); // no really required anymore
			await _driveWatcher.CheckDriveStatusAsync(_store.Drives.ToList());

			CheckDrivesConnectionStatus();

			_timer.Enabled = true;
		}

		private void CheckDrivesConnectionStatus()
		{
			if (!isScanDoneOnce)
			{
				isScanDoneOnce = true;
				return;
			}
			foreach (var drive in _store.Drives)
			{
				if (drive.Status.IsHostAvailable && !drive.Status.IsConnected && drive.Options.AutoConnectIfAvailable && Settings.IsAutoConnectIfAvailable)
				{
					DriveHelper.Add(drive);
				}
				else if (!drive.Status.IsHostAvailable && drive.Status.IsConnected)
				{
					DriveHelper.Remove(drive);
				}
			}
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

				GetDrivesAndRegisterToStore();
				RegisterStoreDrivesToHostWatcher();

				// Start
				_timer.Start();

				IsEnabled = true;
				return true;
			}
			Log.Warning("Start aborted, already running");
			return false;
		}

		private void RegisterStoreDrivesToHostWatcher()
		{
			var listOfHosts = _store.GetRegisteredHostsOREMPTY();
			foreach (var hostOrIP in listOfHosts)
			{
				_hostWatcher.RegisterHost(hostOrIP);
				Log.Debug("Host {host} added to ping monitor", hostOrIP);
			}
			Log.Debug("Added {count} hosts to ping watch", _hostWatcher.HostCount);
		}

		private void GetDrivesAndRegisterToStore()
		{
			var drivesOfConfigFile = DataAccess.GetDrives0REmptyList();
			foreach (var drive in drivesOfConfigFile)
			{
				_store.Register(drive);
			}
		}

		public bool Deactivate()
		{
			if (!IsEnabled)
			{
				// Stop
				_timer.Stop();
				_hostWatcher.Clear();
				_store.Clear();

				IsEnabled = false;
				return true;
			}
			Log.Warning("Stop aborted, not running yet");
			return false;
		}

		#endregion
	}
}