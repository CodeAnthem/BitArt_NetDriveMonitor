using ControlzEx.Theming;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using NetDriveManager.WPF.utilities.contentController.config;
using NetDriveManager.WPF.utilities.contentController.services;
using System.Windows;

namespace NetDriveManager.WPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		// I could probably also create the service provider within the ctor and feed a private field

		private static void SetupServiceProvider()
		{
			Ioc.Default.ConfigureServices(
				ServicesConfigurator.GetServices()
				.BuildServiceProvider());
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			SetupServiceProvider();
			using (var scope = Ioc.Default.CreateScope())
			{
				scope.ServiceProvider.GetRequiredService<IControllerConfig>()
					.RegisterAll();

				var cc = scope.ServiceProvider.GetRequiredService<IContentControllerService>();
				var mainWindow = cc.GetWindow(nameof(MainWindow));
				mainWindow.Show();

				base.OnStartup(e);

				ThemeManager.Current.ChangeTheme(this, "Light.Steel");
			}
		}
	}
}