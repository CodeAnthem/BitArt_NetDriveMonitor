using NetDriveManager.Monitor.Interfaces;
using NetDriveManager.Monitor.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace NetDriveManager.Monitor.components.NetDriveWatcher
{
	public class NetDriveWatcherTimer
	{
		#region Private Fields

		private readonly INetDriveHelper _driveHelper;
		private readonly INetDriveMonitorSettings _settings;
		private readonly INetDriveStore _store;
		private readonly System.Timers.Timer _timer = new Timer();

		#endregion

		#region Public Constructors

		public NetDriveWatcherTimer(INetDriveStore store, INetDriveHelper driveHelper, INetDriveMonitorSettings settings)
		{
			TimerSetup();
			_store = store;
			_driveHelper = driveHelper;
			_settings = settings;
		}

		#endregion

		#region Public Methods

		public void Start()
		{
			_timer.Enabled = true;
			_timer.Start();
		}

		public void Stop()
		{
			_timer.Enabled = false;
			_timer.Stop();
		}

		#endregion

		#region Private Methods

		private void CheckNetDrivesAndUpdateStatus()
		{
			var connectedLocalDrives = Environment.GetLogicalDrives();
			foreach (var drive in _store.Drives)
			{
				bool isConnected = IsNetDriveConnected(drive, connectedLocalDrives);
				if (drive.Status.IsConnected != isConnected)
				{
					drive.Status.IsConnected = isConnected;
					Log.Information("Drive: {drive} Status IsConnected Updated to: {status}", drive, isConnected);
				}
				else
				{
					// check if drive has to be dis/connecting
					if (drive.Status.IsHostAvailable && !drive.Status.IsConnected && drive.Options.AutoConnectIfAvailable && _settings.IsAutoConnectIfAvailable)
					{
						_driveHelper.Add(drive);
					}
					else if (!drive.Status.IsHostAvailable && drive.Status.IsConnected)
					{
						_driveHelper.Remove(drive);
					}
				}
			}
		}

		private bool IsNetDriveConnected(INetDrive drive, string[] connectedLocalDrives)
		{
			foreach (var localDriveLetter in connectedLocalDrives)
			{
				if (localDriveLetter.Substring(0, 1).ToLower() == drive.Info.Letter.ToLower())
				{
					return true;
				}
			}
			return false;
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			_timer.Stop();
			CheckNetDrivesAndUpdateStatus();
			_timer.Start();
		}

		private void TimerSetup()
		{
			_timer.Interval = 1000;
			_timer.AutoReset = true;
			_timer.Elapsed += Timer_Elapsed;
		}

		#endregion
	}
}