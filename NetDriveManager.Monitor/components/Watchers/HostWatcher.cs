using BitArt_WindowsHelpers.Network;
using NetDriveManager.Monitor.Interfaces;
using NetDriveManager.Monitor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetDriveManager.Monitor.components.Watchers
{
	public class HostWatcher : IHostWatcher
	{
		public event HostNameChangedEventHandler OnHostChanged;

		public delegate void HostNameChangedEventHandler(string hostName, bool state);

		public bool NotifyOnChangesOnly { get; set; }
		public bool NotifyOnFirstPing { get; set; }
		public int HostCount { get => _hostsList.Count; }
		private readonly List<HostWatcherModel> _hostsList = new List<HostWatcherModel>();

		public bool RegisterHost(string nameOrAddress)
		{
			bool alreadyInList = false;

			foreach (var host in _hostsList)
			{
				if (host.HostName == nameOrAddress)
				{
					alreadyInList = true;
				}
			}

			if (!alreadyInList)
			{
				var newHost = new HostWatcherModel(nameOrAddress);
				_hostsList.Add(newHost);
				return true;
			}
			else
			{
				return false;
			}
		}

		public void Clear() => _hostsList.Clear();

		public async Task ScanAllHostsAsyncParallel()
		{
			await Task.Run(() =>
			{
				Parallel.ForEach<HostWatcherModel>(_hostsList, async (host) =>
				{
					//var pinger = new MyPing();
					bool response1 = await MyPing.PingHostNewAsync(host.HostName); // ping
					bool response2 = await MyPing.PingHostNewAsync(host.HostName); // ping

					if (response1 == response2)
					{
						SendUpdateIfRequired(host, response2);
					}
				});
			});
		}

		private void SendUpdateIfRequired(HostWatcherModel host, bool isOnline)
		{
			bool sendUpdate;

			if (!host.DoneOnce && NotifyOnFirstPing)
			{
				host.DoneOnce = true;
				sendUpdate = true;
			}
			else
			{
				bool didStatusChange = isOnline != host.Status;
				if (NotifyOnChangesOnly)
					sendUpdate = didStatusChange;
				else
					sendUpdate = true;
			}

			host.Status = isOnline;
			if (sendUpdate)
				OnHostChanged?.Invoke(host.HostName, host.Status);
		}
	}
}