using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace NetDriveMonitor.helpers
{
	public static class NetworkHelper
	{
		public static void PingHostAsync(string nameOrAddress, Action<bool> del)
		{
			Ping pinger = null;
			AutoResetEvent autoResetEvent = new AutoResetEvent(false);

			pinger = new Ping();
			pinger.PingCompleted += (object sender, PingCompletedEventArgs e) =>
			{
				if (e.Reply?.Status == IPStatus.Success)
				{
					del(true);
				}
				else
				{
					del(false);
				}
			((AutoResetEvent)e.UserState).Set();
			};

			pinger.SendAsync(nameOrAddress, 200, Encoding.ASCII.GetBytes(""), autoResetEvent);

			//pinger.SendPingAsync(nameOrAddress, 200);
			autoResetEvent.WaitOne();
			pinger.Dispose();
		}

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
			catch (Exception)
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