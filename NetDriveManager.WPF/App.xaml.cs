using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using NetDriveManager.WPF.utilities.navigation.config;
using NetDriveManager.WPF.utilities.navigation.services;
using System.Windows;

namespace NetDriveManager.WPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			// Setup of DI Container
			var serviceCollection = ServicesConfigurator.Configure();
			var serviceProvider = serviceCollection.BuildServiceProvider();
			Ioc.Default.ConfigureServices(serviceProvider);
		}

		protected override async void OnStartup(StartupEventArgs e)
		{
			using (var scope = Ioc.Default.CreateScope())
			{
				var navConfig = scope.ServiceProvider.GetRequiredService<NavigationConfig>();
				var nav = scope.ServiceProvider.GetRequiredService<INavigationService>();
				await nav.ShowWindowAsync(nameof(MainWindow));
				base.OnStartup(e);
			}
		}
	}
}