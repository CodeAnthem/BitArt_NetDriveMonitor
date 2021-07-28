using BitArt_Netdrive_Helpers;
using BitArt_Netdrive_Helpers.enums;
using BitArt_Netdrive_Helpers.interfaces;
using BitArt_Network_Helpers;
using NetDriveMonitor.components;
using NetDriveMonitor.interfaces;
using NetDriveMonitor.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace NetDriveMonitor
{
	public class NDMCore : NDMBase
	{
		public bool IsRunning { get; set; } // global app state
		public IDatastore DataStore { get; set; }
		public HostMonitor HostMonitor { get; set; } = new HostMonitor();
		public NetdriveBuddy NDBuddy { get; set; } = new NetdriveBuddy();

		private readonly string _saveFilePath = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}" +
												@"\Save\drives.json";

		public bool Start()
		{
			if (!IsRunning)
			{
				// start app
				GetData();
				RemoveUnavailibleDrives();
				RegisterDrives();

				HostMonitor.Start();

				IsRunning = true;
				return true;
			}
			else
			{
				HostMonitor.Stop();
				IsRunning = false;
				return false;
			}
		}

		private void GetData()
		{
			ConfiguredDriveList.Clear();
			ConfiguredDriveList = DataStore.Get();
			Debug.WriteLine($"[Net Drive Monitor] Loaded {ConfiguredDriveList.Count} configured drives");
		}

		private void RegisterDrives()
		{
			HostMonitor.Clear(); // clear HostMonitor cache
			RegistredDriveDict.Clear();

			foreach (var drive in ConfiguredDriveList)
			{
				if (drive.AutoReconnect)
				{
					RegisterDriveToDict(drive);
					HostMonitor.AddHost(drive.HostName);
				}
			}
			Debug.WriteLine($"[Net Drive Monitor] Registred {HostMonitor.HostCount} registred drives on host monitor");
		}

		private void RemoveUnavailibleDrives()
		{
			int removedDrivesCounter = 0;
			foreach (var drive in NDBuddy.GetAll())
			{
				if (drive.Status == NetdriveStatus.Disconnected)
				{
					var configuredDrive = ConfiguredDriveList.Find(d => d.Letter == drive.Letter);
					if (configuredDrive != null)
					{
						var removeStatus = NDBuddy.Remove(drive);
						if (removeStatus)
						{
							drive.Status = NetdriveStatus.NotFound;
							removedDrivesCounter++;
						}
					}
				}
			}
			Debug.WriteLine($"[Net Drive Monitor] Removed {removedDrivesCounter} unavailible drives");
		}

		private NetdriveStatus GetStatus(NetDriveMonitorModel drive)
		{
			var currentDrives = NDBuddy.GetAll();
			var foundDrive = currentDrives.FirstOrDefault(d => d.Letter == drive.Letter && d.HostName == drive.HostName && d.Share == drive.Share);
			drive.Status = foundDrive == null ? NetdriveStatus.NotFound : foundDrive.Status;
			return drive.Status;
			//return (foundDrive?.Status) ?? NetdriveStatus.Unknown;
			//return foundDrive == null ? NetdriveStatus.Unknown : foundDrive.Status;
		}

		public bool Stop()
		{
			if (IsRunning)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public NDMCore()
		{
			DataStore = new DataStoreJson(_saveFilePath, true);
			HostMonitor.NotifyOnChangesOnly = false;
			HostMonitor.NotifyOnFirstPing = true;
			HostMonitor.OnHostChanged += CheckDrivesOfHost;
		}

		private void CheckDrivesOfHost(string hostName, bool state)
		{
			Debug.WriteLine($"[Net Drive Monitor] Host Update: (host: {hostName} - isOnline: {state}");

			if (DoesContainHostKey(hostName))
			{
				var drivesOfHostList = RegistredDriveDict[hostName];
				foreach (var drive in drivesOfHostList)
				{
					GetStatus(drive);
					if (drive.Status == NetdriveStatus.NotFound && state)
					{
						var result = NDBuddy.Add(drive);
						drive.Status = result ? NetdriveStatus.Connected : drive.Status;
					}
					if (drive.Status == NetdriveStatus.Disconnected && !state)
					{
						var result = NDBuddy.Remove(drive);
						drive.Status = result ? NetdriveStatus.Disconnected : drive.Status;
					}
				}
			}
		}
	}
}