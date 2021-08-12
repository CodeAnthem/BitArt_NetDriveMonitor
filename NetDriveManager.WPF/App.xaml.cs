using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using NetDriveManager.WPF.utilities.contentController.config;
using NetDriveManager.WPF.utilities.contentController.services;
using System;
using System.Windows;

namespace NetDriveManager.WPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public new static App Current => (App)Application.Current;

		private IServiceProvider Services { get; }

		public App()
		{
			Services = ServicesConfigurator.GetServices().BuildServiceProvider();
			Ioc.Default.ConfigureServices(Services);
			InitializeComponent();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			using (IServiceScope scope = Services.CreateScope())
			{
				scope.ServiceProvider.GetRequiredService<IControllerConfig>()
					.RegisterAll();
				base.OnStartup(e);

				var cc = scope.ServiceProvider.GetRequiredService<IContentControllerService>();
				var mainWindow = cc.GetWindow(nameof(MainWindow));
				mainWindow.Show();
			}
		}
	}
}