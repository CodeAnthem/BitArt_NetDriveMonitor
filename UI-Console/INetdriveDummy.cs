using System;
using System.Collections.Generic;
using System.Text;

namespace UI_Console
{
	internal interface INetdriveDummy
	{
		public string Letter { get; set; }
		public string HostName { get; set; }
		public string Share { get; set; }
		public bool AutoReConnectEnabled { get; set; }
	}
}