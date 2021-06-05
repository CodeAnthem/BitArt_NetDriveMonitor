using NetDriveMonitor.helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveMonitor
{
	public class NTMonitor
	{
		public NetDriveHelper NDHelper { get; set; }

		public NTMonitor()
		{
			NDHelper = new NetDriveHelper();
		}
	}
}