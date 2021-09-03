using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NetDriveManager.Monitor.components.NetDriveStore
{
	public class NetDriveStore : INetDriveStore
	{
		#region Private Methods

		private void AddDriveToHostDict(NetdriveMonitorModel drive)
		{
			string hostnameOrIP = drive.HostName;
			AddHostIfNotExistYet(hostnameOrIP);
			DrivesByHostDict[hostnameOrIP].Add(drive);
		}

		private void AddHostIfNotExistYet(string hostnameOrIP)
		{
			if (!IsHostAlreadyKnown(hostnameOrIP))
			{
				DrivesByHostDict.Add(hostnameOrIP, new List<NetdriveMonitorModel>());
				Log.Debug("Host '{host}' added", hostnameOrIP);
				return;
			}
			Log.Debug("Host '{host}' already known", hostnameOrIP);
		}

		private bool IsHostAlreadyKnown(string hostnameOrIP) => DrivesByHostDict.ContainsKey(hostnameOrIP);

		#endregion

		#region Public Properties

		public ObservableCollection<NetdriveMonitorModel> Drives { get; } = new ObservableCollection<NetdriveMonitorModel>();
		public Dictionary<string, List<NetdriveMonitorModel>> DrivesByHostDict { get; set; } = new Dictionary<string, List<NetdriveMonitorModel>>();

		#endregion

		#region Public Methods

		public void Clear()
		{
			Drives.Clear();
			DrivesByHostDict.Clear();
			Log.Debug("Cleared store");
		}

		public List<NetdriveMonitorModel> GetDrivesOfHostOREMPTY(string hostnameOrIP)
		{
			List<NetdriveMonitorModel> drives = IsHostAlreadyKnown(hostnameOrIP)
				? DrivesByHostDict[hostnameOrIP]
				: new List<NetdriveMonitorModel>();
			Log.Debug("Found {count} drives for host: {host}", drives.Count, hostnameOrIP);
			return drives;
		}

		public List<NetdriveMonitorModel> GetDrivesOfHostOREMPTY2(string hostnameOrIP)
		{
			// Alternate Version
			List<NetdriveMonitorModel> drives = new List<NetdriveMonitorModel>();
			if (IsHostAlreadyKnown(hostnameOrIP))
			{
				drives = DrivesByHostDict[hostnameOrIP];
			}
			Log.Debug("Found {count} drives for host: {host}", drives.Count, hostnameOrIP);
			return drives;
		}

		public List<string> GetRegisteredHostsOREMPTY()
		{
			var listOfHosts = new List<string>();
			if (DrivesByHostDict.Count > 0)
			{
				listOfHosts = DrivesByHostDict.Keys.ToList();
			}
			Log.Debug("Found {count} registered hosts");
			return listOfHosts;
		}

		public void Register(NetdriveMonitorModel drive)
		{
			//if (drive.Equals(null))
			//{
			//	throw new Exception("Drive cant be empty");
			//}
			if (drive == null)
			{
				throw new Exception("Drive cant be empty");
			}

			Drives.Add(drive);
			AddDriveToHostDict(drive);
		}

		#endregion
	}
}