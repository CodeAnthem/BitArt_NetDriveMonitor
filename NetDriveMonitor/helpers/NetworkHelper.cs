﻿using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace NetDriveMonitor.helpers
{
	public static class NetworkHelper
	{
		public static bool PingHost(string nameOrAddress)
		{
			bool pingable = false;
			Ping pinger = null;

			try
			{
				pinger = new Ping();

				PingReply reply = pinger.Send(nameOrAddress, 200);
				pingable = reply.Status == IPStatus.Success;
			}
			catch (Exception e)
			{
			}
			finally
			{
				if (pinger != null)
				{
					pinger.Dispose();
				}
			}

			return pingable;
		}
	}
}