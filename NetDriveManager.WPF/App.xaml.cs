using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using NetDriveManager.WPF.services;
using NetDriveManager.WPF.viewModels;
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
			Ioc.Default.ConfigureServices(serviceCollection.BuildServiceProvider());
		}

		protected override async void OnStartup(StartupEventArgs e)
		{
			using (var scope = Ioc.Default.CreateScope())
			{
				var nav = scope.ServiceProvider.GetRequiredService<INavigationService>();
				await nav.ShowAsync(nameof(MainWindow));
				base.OnStartup(e);
			}

			//var viewModel = Ioc.Default.GetService<MainWindowViewModel>();

			//var nav = Ioc.Default.GetRequiredService<INavigationService>();
			//await nav.ShowAsync(nameof(MainWindow));
			//base.OnStartup(e);

			//using (var scope = _DI.BeginLifetimeScope())
			//{
			//	var serviceProvider = new AutofacServiceProvider(scope);
			//	var nav = scope.Resolve<NavigationService>();

			//		new NavigationService();
			//	//INavigationService initialNavigationService = scope.Resolve<INavigationService>();
			//	//initialNavigationService.Navigate();

			//	// Variant I
			//	MainWindow = scope.Resolve<MainWindow>();
			//	MainWindow.DataContext = scope.Resolve<MainWindowViewModel>();
			//	MainWindow.Show();

			// Variant II
			//var window = scope.Resolve<MainWindow>();
			//window.DataContext = scope.Resolve<MainWindowViewModel>();
			//window.Show();

			//TODO: Question: Whats the difference?

			//base.OnStartup(e);
			//}
		}
	}
}