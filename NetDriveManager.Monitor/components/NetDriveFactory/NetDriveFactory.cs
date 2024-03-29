﻿using NetDriveManager.Monitor.Interfaces;
using NetDriveManager.Monitor.Models;
using System;
using System.IO;

namespace NetDriveManager.Monitor.components.NetDriveFactory
{
	public class NetDriveFactory : INetDriveFactory
	{
		#region Public Methods

		public INetDrive Create(string letter, string hostName, string share,
							bool acIfAvailable = false, bool acLanOnly = false)
		{
			var driveInfo = CreateDriveInfo(letter, hostName, share);
			var driveOptions = new NetDriveOptions(acIfAvailable, acLanOnly);
			var driveStatus = new NetDriveStatus();

			var drive = new NetDriveModel(driveInfo, driveOptions, driveStatus);
			return drive;
		}

		public INetDrive Create(NetDriveInfo info, NetDriveOptions options, NetDriveStatus status)
		{
			return new NetDriveModel(info, options, status);
		}

		public INetDrive Create(NetDriveInfo info)
		{
			return new NetDriveModel(info, new NetDriveOptions(), new NetDriveStatus());
		}

		public NetDriveInfo CreateDriveInfo(string letter, string hostName, string share)
		{
			if (string.IsNullOrEmpty(hostName))
				throw new Exception($"Hostname can't be empty: '{hostName}'");

			if (string.IsNullOrEmpty(share))
				throw new Exception($"Share can't be empty: '{share}'");

			if (string.IsNullOrEmpty(letter))
				throw new Exception($"Letter can't be empty: '{letter}'");

			string fixedLetter = FixDriveLetter(letter);

			return new NetDriveInfo(fixedLetter, hostName, share);
		}

		public NetDriveInfo CreateDriveInfo(char letter, string hostName, string share)
		{
			if (string.IsNullOrEmpty(hostName))
				throw new Exception($"Hostname can't be empty: '{hostName}'");

			if (string.IsNullOrEmpty(share))
				throw new Exception($"Share can't be empty: '{share}'");

			if (!char.IsLetter(letter))
				throw new Exception($"Letter is not a letter: '{letter}'");

			return new NetDriveInfo(letter, hostName, share);
		}

		#endregion

		#region Private Methods

		private string FixDriveLetter(string driveLetter)
		{
			if (driveLetter.Length > 1)
				driveLetter = driveLetter.Substring(0, 1);
			return driveLetter;
		}

		#endregion
	}
}