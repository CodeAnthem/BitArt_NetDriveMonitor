using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveManager.Monitor.components.netDrivePingWatchdog.parts
{
	public class NetdrivePingWatchdogReportModel
	{
		public NetdrivePingWatchdogReportModel(NetdriveMonitorModel drive, bool isOnline)
		{
			Drive = drive;
			IsOnline = isOnline;
		}

		public NetdriveMonitorModel Drive { get; }
		public bool IsOnline { get; }
	}
}