using NetDriveManager.Monitor.components.NetDriveMounter;
using NetDriveManager.Monitor.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

		public bool Add(INetDrive drive) => AddAction(drive);

		// not really used, but why not?
		public bool Add(string letter, string hostName, string share)
		{
			INetDrive drive = _factory.Create(letter, hostName, share);
			if (drive is null)
				throw new Exception("Failed to create drive");
			return AddAction(drive);
		}

		public async Task<bool> AddAsync(INetDrive drive) => await Task.Run(() => AddAction(drive));

		public IEnumerable<INetDrive> GetAll() => _directMounter.GetAll();

		public async Task<IEnumerable<INetDrive>> GetAllAsync() => await Task.Run(() => _directMounter.GetAll());

		public bool IsAlreadyConnected(INetDrive drive) => IsAlreadyConnectedAction(drive.Info.Letter);

		public bool IsAlreadyConnected(string letter) => IsAlreadyConnectedAction(letter);

		public bool Remove(INetDrive drive) => RemoveAction(drive.Info.Letter);

		public bool Remove(string letter) => RemoveAction(letter);

		public bool RemoveAll() => _cmdMounter.RemoveAll();

		public async Task<bool> RemoveAllAsync() => await Task.Run(() => _cmdMounter.RemoveAll());

		public async Task<bool> RemoveAsync(INetDrive drive) => await Task.Run(() => RemoveAction(drive.Info.Letter));

		#endregion

		#region Private Methods

		private bool AddAction(INetDrive drive) => _cmdMounter.Add(drive);

		private bool IsAlreadyConnectedAction(string letter) => IsLetterUsed(letter);

		private bool IsAlreadyConnectedAction(char letter) => IsLetterUsed(letter);

		private bool RemoveAction(string letter) => _cmdMounter.Remove(letter);

		private bool RemoveAction(char letter) => _cmdMounter.Remove(letter);

		#endregion
	}
}