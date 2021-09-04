using BitArt_Network_Helpers;
using NetDriveManager.Monitor.Interfaces;
using Serilog;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NetDriveManager.Monitor
{
	public class NetDriveMonitor : INetDriveMonitor
	{
		#region Private Fields

		private readonly IDataAccess _da;

		private readonly INetDriveHelper _helper;
		private readonly IHostMonitor _hostMonitor;
		private readonly ILogger _logger;
		private readonly INetDriveStore _store;

		#endregion

		#region Public Properties

		public ObservableCollection<INetDrive> Drives { get => _store.Drives; }

		public bool IsEnabled { get; private set; }

		#endregion

		#region Public Constructors

		public NetDriveMonitor(ILogger logger, IDataAccess dataAccess, INetDriveStore store, IHostMonitor hostMonitor, INetDriveHelper helper)
		{
			_logger = logger.ForContext<NetDriveMonitor>();
			_da = dataAccess;
			_da.UseDummyDataIfEmpty = true;
			_store = store;
			_hostMonitor = hostMonitor;

			_hostMonitor.ScanInterval = 1000;
			_hostMonitor.NotifyOnChangesOnly = true;
			_hostMonitor.NotifyOnFirstPing = false;

			_hostMonitor.OnHostChanged += HostStatusChanged;
			_helper = helper;

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
					if (drive.Options.AutoConnectIfAvailable)
						ConnectDrive(drive);
				}
				else
				{
					DisconnectDrive(drive);
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

		public bool ConnectDrive(INetDrive drive) => _helper.Add(drive);

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

		public bool DisconnectDrive(INetDrive drive) => _helper.Remove(drive);

		public IEnumerable<INetDrive> GetDrives() => _da.GetDrives0REmptyList();

		public bool SaveDrives(List<INetDrive> drivesList) => _da.SaveDrives(drivesList);

		#endregion
	}
}