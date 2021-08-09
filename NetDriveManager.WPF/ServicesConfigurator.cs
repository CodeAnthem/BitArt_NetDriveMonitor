using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.utilities.navigation;
using NetDriveManager.WPF.viewModels;

namespace NetDriveManager.WPF
{
	public static class ServicesConfigurator
	{
		public static IServiceCollection Configure()
		{
			var serviceCollection = new ServiceCollection();

			// Navigation
			serviceCollection.AddSingleton<INavigationService>(new NavigationService());
			serviceCollection.AddSingleton<INavigationConfig>(sc =>
			{
				return new NavigationConfig(sc, sc.GetRequiredService<INavigationService>());
			});

			// View Models
			serviceCollection.AddSingleton<MainWindowViewModel>();
			serviceCollection.AddSingleton<HeaderControlViewModel>();

			// Windows & Views
			serviceCollection.AddTransient<MainWindow>();
			serviceCollection.AddTransient<HeaderControl>();

			return serviceCollection;
		}
	}
}