using BitArt_Network_Helpers;
using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.Monitor.components.dataAccess;
using NetDriveManager.Monitor.components.netdriveHandler;
using NetDriveManager.Monitor.components.NetDriveStore;

namespace NetDriveManager.Monitor
{
	public static class ServiceCollectionExtension
	{
		#region Public Methods

		public static IServiceCollection AddNetdriveManager(this IServiceCollection services)
		{
			services.AddSingleton<INetdriveMonitor, NetdriveMonitor>();

			services.AddSingleton<IDataAccess, DataAccess>();
			services.AddSingleton<IDataAccessor, DataAccessorJsonFile>();
			services.AddSingleton<INetdriveHandler, NetdriveHandler>();
			services.AddSingleton<INetDriveStore, NetDriveStore>();
			services.AddSingleton<IHostMonitor, HostMonitor>();

			return services;
		}

		#endregion
	}
}