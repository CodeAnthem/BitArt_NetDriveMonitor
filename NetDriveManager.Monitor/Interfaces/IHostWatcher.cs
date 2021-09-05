using NetDriveManager.Monitor.components.Watchers;
using System.Threading.Tasks;

namespace NetDriveManager.Monitor.Interfaces
{
	public interface IHostWatcher
	{
		int HostCount { get; }
		bool NotifyOnChangesOnly { get; set; }
		bool NotifyOnFirstPing { get; set; }

		event HostWatcher.HostNameChangedEventHandler OnHostChanged;

		void Clear();

		bool RegisterHost(string nameOrAddress);

		Task ScanAllHostsAsyncParallel();
	}
}