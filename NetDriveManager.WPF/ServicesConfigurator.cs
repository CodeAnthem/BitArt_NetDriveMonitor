using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.WPF.controls;
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
			var serviceCollection = new ServiceCollection();

			// Content Control
			serviceCollection.AddSingleton<IContentStore, ContentStore>();
			serviceCollection.AddSingleton<IContentControllerService, ContentControllerService>();
			serviceCollection.AddSingleton<IControllerConfig, ControllerConfig>();
			//serviceCollection.AddSingleton<IControllerConfig, ControllerConfig>(sc =>
			//	new ControllerConfig(sc, sc.GetRequiredService<IContentStore>()));

			// View Models
			serviceCollection.AddSingleton<MainWindowViewModel>();
			serviceCollection.AddSingleton<SettingsViewModel>();
			serviceCollection.AddSingleton<HeaderControlViewModel>();

			// Windows & Views
			serviceCollection.AddTransient<MainWindow>();
			serviceCollection.AddTransient<SettingsWindow>();
			serviceCollection.AddTransient<HeaderControl>();

			return serviceCollection;
		}
	}
}