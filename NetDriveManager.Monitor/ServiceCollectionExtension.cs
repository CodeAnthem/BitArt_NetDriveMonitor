using BitArt_Network_Helpers;
using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.Monitor.components.dataAccess;
using NetDriveManager.Monitor.components.netdriveHandler;
using NetDriveManager.Monitor.components.netDrivePingWatchdog;

namespace NetDriveManager.Monitor
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddNetdriveManager(this IServiceCollection services)
		{
			services.AddSingleton<INetdriveMonitor, NetdriveMonitor>();

			services.AddSingleton<IDataAccess, DataAccess>();
			services.AddSingleton<IDataAccessor, DataAccessorJsonFile>();
			services.AddSingleton<INetdriveHandler, NetdriveHandler>();
			services.AddSingleton<INetdrivePingWatchdog, NetdrivePingWatchdog>();
			services.AddSingleton<INetdrivePingWatchdogStore, NetdrivePingWatchdogStore>();
			services.AddSingleton<IHostMonitor, HostMonitor>();

			return services;
		}
	}
}