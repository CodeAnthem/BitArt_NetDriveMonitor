using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.Monitor;
using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.helpers;
using NetDriveManager.WPF.utilities.contentController.config;
using NetDriveManager.WPF.utilities.contentController.services;
using NetDriveManager.WPF.utilities.contentController.stores;
using NetDriveManager.WPF.viewModels;
using NetDriveManager.WPF.views;

namespace NetDriveManager.WPF
{
	public static class ServicesConfigurator
	{
		public static IServiceCollection GetServices()
		{
			var services = new ServiceCollection();

			#region Content Control

			services.AddSingleton<IContentStore, ContentStore>();
			services.AddSingleton<IContentControllerService, ContentControllerService>();
			services.AddSingleton<IControllerConfig, ControllerConfig>();
			//serviceCollection.AddSingleton<IControllerConfig, ControllerConfig>(sc =>
			//	new ControllerConfig(sc, sc.GetRequiredService<IContentStore>()));

			#endregion Content Control

			#region View Models

			services.AddSingleton<MainWindowViewModel>();

			services.AddSingleton<HeaderControlViewModel>();
			services.AddSingleton<FooterPartViewModel>();

			services.AddSingleton<OverviewViewModel>();
			services.AddSingleton<SettingsViewModel>();
			services.AddSingleton<ManageDrivesViewModel>();

			#endregion View Models

			#region Views

			services.AddTransient<MainWindow>();

			services.AddTransient<HeaderPartControl>();
			services.AddTransient<FooterPartControl>();

			services.AddTransient<OverviewControl>();
			services.AddTransient<SettingsControl>();
			services.AddTransient<ManageDrivesControl>();

			#endregion Views

			services.AddSingleton<MainContentStore>();

			// Other Libraries
			services.AddNetdriveManager();

			// Logging
			services.AddSerilogLogging();

			return services;
		}
	}
}