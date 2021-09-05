using BitArt_WindowsHelpers.Network;
using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.Monitor.components.dataAccess;
using NetDriveManager.Monitor.components.NetDriveFactory;
using NetDriveManager.Monitor.components.NetDriveHelper;
using NetDriveManager.Monitor.components.NetDriveMounter;
using NetDriveManager.Monitor.components.NetDriveStore;
using NetDriveManager.Monitor.components.SettingsFactory;
using NetDriveManager.Monitor.components.Watchers;
using NetDriveManager.Monitor.Interfaces;

namespace NetDriveManager.Monitor
{
	public static class ServiceCollectionExtension
	{
		#region Public Methods

		public static IServiceCollection AddNetdriveManager(this IServiceCollection services)
		{
			// Main Program
			services.AddSingleton<INetDriveMonitor, NetDriveMonitor>();

			// Settings
			var settings = SettingsFactory.Create();
			services.AddSingleton<INetDriveMonitorSettings>(settings);

			// Data Access
			services.AddSingleton<IDataAccess, DataAccess>();
			services.AddSingleton<IDataAccessor, DataAccessorJsonFile>();

			// Factory
			services.AddScoped<INetDriveFactory, NetDriveFactory>();

			// Helper
			services.AddScoped<INetDriveHelper, NetDriveHelper>();

			// Mounter
			services.AddScoped<INetDriveMounter<NetDriveMounterCMD>, NetDriveMounterCMD>();
			services.AddScoped<INetDriveMounter<NetDriveMounterDirect>, NetDriveMounterDirect>();

			// Store
			services.AddSingleton<INetDriveStore, NetDriveStore>();

			// Watchers
			services.AddSingleton<NetDriveWatcher>();
			services.AddSingleton<IHostWatcher, HostWatcher>();

			// Helpers > external
			services.AddSingleton<IPathFinderUNC, PathFinderUNC>();

			return services;
		}

		#endregion
	}
}