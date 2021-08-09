using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.utilities.navigation.config;
using NetDriveManager.WPF.utilities.navigation.services;
using NetDriveManager.WPF.viewModels;
using NetDriveManager.WPF.views;

namespace NetDriveManager.WPF
{
	public static class ServicesConfigurator
	{
		#region Public Methods

		public static IServiceCollection Configure()
		{
			var serviceCollection = new ServiceCollection();

			// Navigation
			serviceCollection.AddSingleton<INavigationStore>(new NavigationStore());
			serviceCollection.AddSingleton<NavigationConfig>(sc =>
			{
				return new NavigationConfig(sc, sc.GetRequiredService<INavigationStore>());
			});
			serviceCollection.AddSingleton<INavigationService>(sc =>
			{
				return new NavigationService(sc.GetRequiredService<INavigationStore>());
			});

			// View Models
			serviceCollection.AddSingleton<MainWindowViewModel>();
			serviceCollection.AddSingleton<HeaderControlViewModel>();
			serviceCollection.AddSingleton<SettingsViewModel>();

			// Windows & Views
			serviceCollection.AddTransient<MainWindow>();
			serviceCollection.AddTransient<HeaderControl>();
			serviceCollection.AddTransient<SettingsWindow>();

			return serviceCollection;
		}

		#endregion Public Methods
	}
}