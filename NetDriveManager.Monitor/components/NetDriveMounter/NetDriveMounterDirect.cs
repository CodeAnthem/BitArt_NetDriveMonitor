using BitArt_WindowsHelpers.Network;
using NetDriveManager.Monitor.Interfaces;
using NetDriveManager.Monitor.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace NetDriveManager.Monitor.components.NetDriveMounter
{
	public class NetDriveMounterDirect : NetDriveMounterBase, INetDriveMounter<NetDriveMounterDirect>
	{
		#region Private Fields

		private readonly INetDriveFactory _factory;
		private readonly IPathFinderUNC _pathFinder;

		#endregion

		#region Public Constructors

		public NetDriveMounterDirect(INetDriveFactory factory, IPathFinderUNC pathFinder)
		{
			_factory = factory;
			_pathFinder = pathFinder;
		}

		#endregion

		#region Public Methods

		public bool Add(INetDrive drive)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<INetDrive> GetAll()
		{
			var driveList = new List<INetDrive>();

			var allDrives = DriveInfo.GetDrives();
			foreach (var drive in allDrives)
			{
				if (drive.DriveType != DriveType.Network)
				{
					continue;
				}
				string remoteFullPath = _pathFinder.GetNetworkPath(drive.Name).Substring(2);

				string[] address = remoteFullPath.Split(@"\", StringSplitOptions.None);
				string hostName = address[0];
				string share = address[1];

				string letter = drive.Name;

				var driveInfo = _factory.CreateDriveInfo(letter, hostName, share);
				var driveStatus = new NetDriveStatus();

				driveStatus.IsConnected = drive.IsReady;

				var netDrive = _factory.Create(driveInfo, new NetDriveOptions(), driveStatus);

				driveList.Add(netDrive);
			}
			Log.Debug("Found {count} drives", driveList.Count);
			return driveList;
		}

		public bool Remove(string letter)
		{
			throw new NotImplementedException();
		}

		public bool Remove(char letter)
		{
			throw new NotImplementedException();
		}

		public bool RemoveAll()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}