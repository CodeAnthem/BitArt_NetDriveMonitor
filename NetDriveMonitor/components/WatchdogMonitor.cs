using NetDriveMonitor.helpers;
using NetDriveMonitor.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Timer = System.Timers.Timer;

namespace NetDriveMonitor.components
{
	public class WatchdogMonitor
	{
		#region Private Fields

		private readonly Timer _timer;
		private readonly bool _isRunning;
		private readonly List<INetDrive> _netDrivesList;
		private readonly Dictionary<string, bool> _serverList;
		private NetDriveHelper _ndHelper;

		#endregion Private Fields

		#region Public Constructors

		public WatchdogMonitor()
		{
			_timer = new Timer();
			_timer.Interval = 1000;
			_timer.Elapsed += OnTimerElapsed;
			_timer.AutoReset = true;
			_timer.Enabled = false;

			_netDrivesList = new List<INetDrive>();
			_serverList = new Dictionary<string, bool>();

			_ndHelper = new NetDriveHelper();
		}

		#endregion Public Constructors

		#region Public Methods

		public void StartMonitoring(List<INetDrive> netDrives)
		{
			if (!_isRunning)
			{
				// read drives
				int amountOfRequiredServers = ReadDrives(netDrives);
				Console.WriteLine("Server found: " + amountOfRequiredServers);

				// start timer
				_timer.Enabled = true;
			}
		}

		public void Stop()
		{
			if (_isRunning)
			{
				_timer.Enabled = false;
			}
		}

		#endregion Public Methods

		#region Private Methods

		private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			CheckServers(); // check which servers are availible
			CheckDrives();
		}

		private int ReadDrives(List<INetDrive> netDrives)
		{
			_serverList.Clear(); // clear list of servers
			_netDrivesList.Clear(); // clear list of net drives

			foreach (var drive in netDrives)
			{
				// add each server address once to the server list
				if (!_serverList.ContainsKey(drive.Server))
				{
					_serverList.Add(drive.Server, false);
				}

				_netDrivesList.Add(drive);
			}

			return _serverList.Count;
		}

		private void CheckDrives()
		{
			// do clean up of mounted but disconnected drives
			// TODO: clean up

			// mount every availible drive
			// TODO: if not connected
			foreach (var drive in _netDrivesList)
			{
				bool isServerAvailible = _serverList[drive.Server];

				if (isServerAvailible)
				{
					bool wasSuccess = _ndHelper.Add(drive);
					if (wasSuccess)
					{
						Console.WriteLine(@$"Drive connected: {drive.Letter} \\{drive.Server}\{drive.Share}");
					}
				}
				else
				{
					bool wasSuccess = _ndHelper.Remove(drive);
					if (wasSuccess)
					{
						Console.WriteLine(@$"Drive disconnected: {drive.Letter} \\{drive.Server}\{drive.Share}");
					}
				}
			}
		}

		private void CheckServers()
		{
			// check every host if availible
			foreach (var host in _serverList.ToList())
			{
				_serverList[host.Key] = NetworkHelper.PingHost(host.Key);
			}
		}

		#endregion Private Methods
	}
}