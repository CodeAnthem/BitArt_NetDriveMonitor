using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using NetDriveManager.WPF.utilities.contentController.config;
using NetDriveManager.WPF.utilities.contentController.services;
using Serilog;
using System;
using System.ComponentModel;
using System.Diagnostics;
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
			Services = ServicesConfigurator.GetServices()
				.BuildServiceProvider();
			Ioc.Default.ConfigureServices(Services);
			InitializeComponent();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			SetupLogger();
			base.OnStartup(e);
			StartApplicationSafeMode();
		}

		private void SetupLogger()
		{
			ILogger logger = Services.GetRequiredService<ILogger>();
			Log.Logger = logger; // setting global log
			Log.Debug("Logging Started");
		}

		private void StartApplicationSafeMode()
		{
			try
			{
				Log.Information("Application starting");
				CreateAppSessionWithDIScope();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Application terminated unexpectedly");
				LoggingConfigurator.LogLine();
				Log.CloseAndFlush();
			}
		}

		private void CreateAppSessionWithDIScope()
		{
			using (IServiceScope scope = Services.CreateScope())
			{
				scope.ServiceProvider.GetRequiredService<IControllerConfig>()
					.RegisterAll();

				var cc = scope.ServiceProvider.GetRequiredService<IContentControllerService>();
				var mainWindow = cc.GetWindow(nameof(MainWindow));
				mainWindow.Closing += OnClosing;
				mainWindow.Show();
			}
		}

		private void OnClosing(object sender, CancelEventArgs e)
		{
			Log.Warning("Application stopped");
			LoggingConfigurator.LogLine();
			Log.CloseAndFlush();
		}
	}
}