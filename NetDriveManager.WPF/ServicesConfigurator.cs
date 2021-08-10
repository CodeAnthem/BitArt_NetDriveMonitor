using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.utilities.contentController.config;
using NetDriveManager.WPF.utilities.contentController.services;
using NetDriveManager.WPF.utilities.contentController.stores;
using NetDriveManager.WPF.utilities.navigation.config;
using NetDriveManager.WPF.utilities.navigation.services;
using NetDriveManager.WPF.viewModels;
using NetDriveManager.WPF.views;

namespace NetDriveManager.WPF
{
	public static class ServicesConfigurator
	{
		public static IServiceCollection GetServices()
		{
			var serviceCollection = new ServiceCollection();

			// Content Control
			serviceCollection.AddSingleton<IContentStore, ContentStore>();
			serviceCollection.AddSingleton<IContentControllerService, ContentControllerService>();

			//serviceCollection.AddSingleton<IControllerConfig, ControllerConfig>();

			serviceCollection.AddSingleton<IControllerConfig, ControllerConfig>(sc =>
				new ControllerConfig(sc, sc.GetRequiredService<IContentStore>()));

			// Navigation
			//serviceCollection.AddSingleton<INavigationStore, NavigationStore>();
			//serviceCollection.AddSingleton<INavigationService, NavigationService>();
			//serviceCollection.AddSingleton<INavigationConfig>(sc =>
			//	new NavigationConfig(sc, sc.GetRequiredService<INavigationStore>()));

			// View Models
			serviceCollection.AddSingleton<MainWindowViewModel>();
			serviceCollection.AddSingleton<SettingsViewModel>();
			serviceCollection.AddSingleton<HeaderControlViewModel>();

			// Windows & Views
			serviceCollection.AddTransient<MainWindow>();
			serviceCollection.AddTransient<HeaderControl>();
			serviceCollection.AddTransient<SettingsWindow>();

			return serviceCollection;
		}
	}
}