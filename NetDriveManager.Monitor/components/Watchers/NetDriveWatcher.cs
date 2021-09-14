using NetDriveManager.Monitor.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetDriveManager.Monitor.components.Watchers
{
	public class NetDriveWatcher
	{
		#region Private Fields

		private readonly INetDriveMonitorSettings _settings;

		#endregion

		#region Public Constructors

		public NetDriveWatcher(INetDriveMonitorSettings settings)
		{
			_settings = settings;
		}

		#endregion

		#region Public Methods

		public async Task CheckDriveStatusAsync(List<INetDrive> drives)
		{
			await Task.Run(() => CheckNetDrivesAndUpdateStatus(drives));
		}

		#endregion

		#region Private Methods

		private bool CheckIfDriveIsAlreadyConnected(INetDrive drive, string[] connectedLocalDrives)
		{
			foreach (var localDriveLetter in connectedLocalDrives)
			{
				char localLetter = localDriveLetter[0];
				if (char.Equals(char.ToUpper(localLetter), char.ToUpper(drive.Info.Letter)))
					return true;
			}
			return false;
		}

		private void CheckNetDrivesAndUpdateStatus(List<INetDrive> drives)
		{
			var connectedLocalDrives = Environment.GetLogicalDrives();
			foreach (var drive in drives)
			{
				bool isConnected = CheckIfDriveIsAlreadyConnected(drive, connectedLocalDrives);
				if (drive.Status.IsConnected != isConnected)
				{
					drive.Status.IsConnected = isConnected;
					Log.Information("Drive: {drive} Status IsConnected Updated to: {status}", drive, isConnected);
				}
			}
		}

		#endregion
	}
}