using BitArt_Netdrive_Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveManager.Monitor.components.netdriveHandler
{
	public class NetdriveHandler : INetdriveHandler
	{
		private static readonly NetdriveBuddy _ndb = new NetdriveBuddy();

		public bool ConnectDrive(NetdriveMonitorModel drive) => _ndb.Add(drive);

		public bool DisconnctAll() => _ndb.RemoveAll();

		public bool DisconnectDrive(NetdriveMonitorModel drive) => _ndb.Remove(drive);

		//public void GetDriveStatus(NetdriveMonitorModel drive) => _;

		//TODO: add get status method
	}
}