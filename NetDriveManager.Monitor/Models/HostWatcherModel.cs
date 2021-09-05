using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveManager.Monitor.Models
{
	public class HostWatcherModel
	{
		public string HostName { get; }
		public bool Status { get; set; }
		public bool DoneOnce { get; set; }

		public HostWatcherModel(string hostName)
		{
			HostName = hostName;
			Status = true;
			DoneOnce = false;
		}
	}
}