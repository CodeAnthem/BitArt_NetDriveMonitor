using NetDriveManager.Monitor.Interfaces;
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

		private void AddDriveToHostDict(INetDrive drive)
		{
			string hostnameOrIP = drive.Info.HostName;
			AddHostIfNotExistYet(hostnameOrIP);
			DrivesByHostDict[hostnameOrIP].Add(drive);
		}

		private void AddHostIfNotExistYet(string hostnameOrIP)
		{
			if (!IsHostAlreadyKnown(hostnameOrIP))
			{
				DrivesByHostDict.Add(hostnameOrIP, new List<INetDrive>());
				Log.Debug("Host '{host}' added", hostnameOrIP);
				return;
			}
			Log.Debug("Host '{host}' already known", hostnameOrIP);
		}

		private bool IsHostAlreadyKnown(string hostnameOrIP) => DrivesByHostDict.ContainsKey(hostnameOrIP);

		#endregion

		#region Public Properties

		public ObservableCollection<INetDrive> Drives { get; } = new ObservableCollection<INetDrive>();
		public Dictionary<string, List<INetDrive>> DrivesByHostDict { get; set; } = new Dictionary<string, List<INetDrive>>();

		#endregion

		#region Public Methods

		public void Clear()
		{
			Drives.Clear();
			DrivesByHostDict.Clear();
			Log.Debug("Cleared store");
		}

		public List<INetDrive> GetDrivesOfHostOREMPTY(string hostnameOrIP)
		{
			List<INetDrive> drives = IsHostAlreadyKnown(hostnameOrIP)
				? DrivesByHostDict[hostnameOrIP]
				: new List<INetDrive>();
			Log.Debug("Found {count} drives for host: {host}", drives.Count, hostnameOrIP);
			return drives;
		}

		public List<INetDrive> GetDrivesOfHostOREMPTY2(string hostnameOrIP)
		{
			// Alternate Version
			List<INetDrive> drives = new List<INetDrive>();
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

		public void Register(INetDrive drive)
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