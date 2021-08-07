using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.WPF.services;
using NetDriveManager.WPF.viewModels;

namespace NetDriveManager.WPF
{
	public static class ServicesConfigurator
	{
		public static IServiceCollection Configure()
		{
			var serviceCollection = new ServiceCollection();

			// Navigation
			serviceCollection.AddSingleton<INavigationService>(sc =>
			{
				var navigationService = new NavigationService(sc);
				NavigationConfiguration.Configure(navigationService);

				return navigationService;
			});

			// View Models
			serviceCollection.AddSingleton<ViewModelLocator>();
			serviceCollection.AddSingleton<MainWindowViewModel>();

			// Windows & Views
			serviceCollection.AddTransient<MainWindow>();

			return serviceCollection;
		}
	}
}