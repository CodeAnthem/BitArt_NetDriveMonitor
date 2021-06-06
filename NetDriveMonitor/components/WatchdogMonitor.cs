using NetDriveMonitor.helpers;
using NetDriveMonitor.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetDriveMonitor.components
{
	public class WatchdogMonitor
	{
		#region Private Fields

		private List<NetDriveModel> _drives;
		private Dictionary<string, bool> _serverList;

		#endregion Private Fields

		#region Public Constructors

		public WatchdogMonitor()
		{
			_drives = new List<NetDriveModel>();
			_serverList = new Dictionary<string, bool>();
		}

		#endregion Public Constructors

		#region Public Methods

		public int ReadDrives(List<NetDriveModel> drives)
		{
			_serverList.Clear(); // clear list of servers

			foreach (var drive in drives)
			{
				if (!_serverList.ContainsKey(drive.Server))
				{
					_serverList.Add(drive.Server, false);
				}

				if (!_serverList.ContainsKey(drive.VPNServer))
				{
					_serverList.Add(drive.VPNServer, false);
				}

				_drives.Add(drive);
			}

			return _serverList.Count;
		}

		public void CheckDrives()
		{
			CheckServers(); // check which servers are availible

			// do clean up of mounted but disconnected drives
			// TODO: clean up

			// mount every availible drive
			// TODO: if not connected
			foreach (var drive in _drives)
			{
				bool isServerAvailible = _serverList[drive.Server];

				if (isServerAvailible)
				{
					Console.WriteLine($"Drive can be connected: {drive.Letter} {drive.Share}");
				}
				else
				{
					Console.WriteLine($"Drive not availible: {drive.Letter} {drive.Share}");
				}
			}
		}

		#endregion Public Methods

		#region Private Methods

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