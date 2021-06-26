using NetDriveMonitor.helpers;
using NetDriveMonitor.interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Timer = System.Timers.Timer;

namespace NetDriveMonitor.components
{
	public class HostnameWatchdog
	{
		public delegate void HostNameChangedEventHandler(string hostName, bool state);

		public event HostNameChangedEventHandler OnHostChanged;

		public bool IsRunning { get; set; }

		//public event EventHandler<string, bool> OnHostNameStatusChanged;
		private readonly Timer _timer;

		private readonly Dictionary<string, bool> _hostsList;

		public HostnameWatchdog()
		{
			_timer = new Timer();
			_timer.Interval = 1000;
			_timer.Elapsed += OnTimerElapsed;
			_timer.AutoReset = true;
			_timer.Enabled = false;

			IsRunning = false;

			_hostsList = new Dictionary<string, bool>();
		}

		public void Start()
		{
			if (!IsRunning)
			{
				_timer.Enabled = true;
				IsRunning = true;
			}
		}

		public void Stop()
		{
			if (IsRunning)
			{
				_timer.Enabled = false;
				IsRunning = false;
			}
		}

		public void Clear()
		{
			_hostsList.Clear();
		}

		public bool AddHost(IHost host)
		{
			if (!_hostsList.ContainsKey(host.HostName))
			{
				_hostsList.Add(host.HostName, false);
				return true;
			}
			return false;
		}

		private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			foreach (var host in _hostsList)
			{
				NetworkHelper.PingHostAsync(host.Key, (bool state) =>
				{
					bool previousState = _hostsList[host.Key]; // saved state

					Debug.WriteLine($"Pinging: {host.Key} state: {state}");

					if (previousState != state)
					{
						OnHostChanged?.Invoke(host.Key, state);
					}

					_hostsList[host.Key] = state;
				});
			}
		}
	}
}