using NetDriveManager.Monitor.components.NetDriveMounter;
using NetDriveManager.Monitor.Interfaces;
using System;
using System.Collections.Generic;

namespace NetDriveManager.Monitor.components.NetDriveHelper
{
	public class NetDriveHelper : NetDriveMounterBase, INetDriveHelper
	{
		#region Private Fields

		private readonly INetDriveMounter<NetDriveMounterCMD> _cmdMounter;
		private readonly INetDriveMounter<NetDriveMounterDirect> _directMounter;
		private readonly INetDriveFactory _factory;

		#endregion

		#region Public Constructors

		public NetDriveHelper(INetDriveMounter<NetDriveMounterDirect> directMounter, INetDriveMounter<NetDriveMounterCMD> cmdMounter, INetDriveFactory factory)
		{
			_directMounter = directMounter;
			_cmdMounter = cmdMounter;
			_factory = factory;
		}

		#endregion

		#region Public Methods

		public bool Add(INetDrive drive) => _cmdMounter.Add(drive);

		public bool Add(string letter, string hostName, string share)
		{
			INetDrive drive = _factory.Create(letter, hostName, share);
			if (drive is null)
				throw new Exception("Failed to create drive");
			return Add(drive);
		}

		public IEnumerable<INetDrive> GetAll() => _directMounter.GetAll();

		public bool IsAlreadyConnected(INetDrive drive) => IsAlreadyConnected(drive.Info.Letter);

		public bool IsAlreadyConnected(string letter) => IsLetterUsed(letter);

		public bool Remove(INetDrive drive) => Remove(drive.Info.Letter);

		public bool Remove(string letter) => _cmdMounter.Remove(letter);

		public bool RemoveAll() => _cmdMounter.RemoveAll();

		#endregion
	}
}