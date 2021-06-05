using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveMonitor.models
{
	public class NetDriveModel
	{
		public NetDriveModel(string letter, string server, string share)
		{
			Letter = letter;
			Server = server;
			Share = share;
		}

		public string Letter { get; set; }
		public string Server { get; set; }
		public string Share { get; set; }
	}
}