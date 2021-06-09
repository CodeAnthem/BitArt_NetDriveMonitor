using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveMonitor.interfaces
{
	public interface INetDrive
	{
		public string Letter { get; set; }
		public string Server { get; set; }
		public string Share { get; set; }
	}
}