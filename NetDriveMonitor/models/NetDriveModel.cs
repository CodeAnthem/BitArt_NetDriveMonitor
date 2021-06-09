using NetDriveMonitor.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveMonitor.models
{
	public class NetDriveModel : INetDrive
	{
		#region Public Properties

		public string Letter { get; set; }

		public string Server { get; set; }

		public string Share { get; set; }

		#endregion Public Properties

		#region Public Constructors

		public NetDriveModel(string letter, string server, string share)
		{
			Letter = letter;
			Server = server;
			Share = share;
		}

		#endregion Public Constructors
	}
}