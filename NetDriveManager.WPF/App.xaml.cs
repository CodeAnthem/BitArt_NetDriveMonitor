using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using NetDriveManager.WPF.utilities.contentController.config;
using NetDriveManager.WPF.utilities.contentController.services;
using Serilog;
using SerilogTimings;
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

		private IServiceProvider _services;

		public App()
		{
			_services = GetServiceProvider();
			SetGlobalContainer();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			SetupGlobalLogger();
			RunAppAndMessureTime();
		}

		private IServiceProvider GetServiceProvider()
		{
			var serviceProvider = new Services_Config().GetMergedServices()
				.BuildServiceProvider();

			return serviceProvider;
		}

		private void SetGlobalContainer()
		{
			Ioc.Default.ConfigureServices(_services);
		}

		private void SetupGlobalLogger()
		{
			Log.Logger = _services.GetRequiredService<ILogger>();
			Log.Debug("Logging Started");
		}

		private void RunAppAndMessureTime()
		{
			using (Operation.Time("App loading"))
			{
				StartApplicationSafeMode();
			}
		}

		private void StartApplicationSafeMode()
		{
			try
			{
				Log.Information("Application starting");
				using (IServiceScope scope = _services.CreateScope())
				{
					scope.ServiceProvider.GetRequiredService<IControllerConfig>()
						.RegisterAll();

					var cc = scope.ServiceProvider.GetRequiredService<IContentControllerService>();
					var mainWindow = cc.GetWindow(nameof(MainWindow));
					mainWindow.Show();
				}
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Application terminated unexpectedly");
				//TODO: Add logline feeature again				LoggingConfigurator.LogLine();
				Log.CloseAndFlush();
			}
		}

		protected override void OnExit(ExitEventArgs e)
		{
			Log.Warning("Application stopped");
			//TODO: Add logline feeature again			LoggingConfigurator.LogLine();
			Log.CloseAndFlush();
		}
	}
}