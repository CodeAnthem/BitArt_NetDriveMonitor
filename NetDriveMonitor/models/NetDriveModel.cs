using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveMonitor.models
{
	public class NetDriveModel
	{
		public NetDriveModel(string letter, string server, string vPNServer, string share)
		{
			Letter = letter;
			Server = server;
			VPNServer = vPNServer;
			Share = share;
		}

		public string Letter { get; set; }
		public string Server { get; set; }
		public string VPNServer { get; set; }
		public string Share { get; set; }
	}
}