using NetDriveMonitor.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveMonitor.components
{
	public class NDMBase
	{
		internal List<NetDriveMonitorModel> ConfiguredDriveList { get; set; } = new List<NetDriveMonitorModel>();
		internal Dictionary<string, List<NetDriveMonitorModel>> RegistredDriveDict { get; set; } = new Dictionary<string, List<NetDriveMonitorModel>>();

		internal void RegisterDriveToDict(NetDriveMonitorModel drive)
		{
			if (!DoesContainHostKey(drive.HostName))
			{
				RegistredDriveDict.Add(drive.HostName, new List<NetDriveMonitorModel>());
			}
			RegistredDriveDict[drive.HostName].Add(drive);
		}

		internal bool DoesContainHostKey(string hostName)
		{
			return RegistredDriveDict.ContainsKey(hostName);
		}
	}
}