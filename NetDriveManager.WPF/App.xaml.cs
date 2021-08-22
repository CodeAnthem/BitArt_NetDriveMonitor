﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using NetDriveManager.WPF.AppUI;
using NetDriveManager.WPF.Main;
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
			_services = new Services_Config().GetMergedServices()
				.BuildServiceProvider();
			Ioc.Default.ConfigureServices(_services);
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			SetupGlobalLogger();
			RegisterContentController();
			RunAppAndMessureTime();
		}

		private void SetupGlobalLogger()
		{
			Log.Logger = _services.GetRequiredService<ILogger>();
			Log.Debug("Logging Started");
		}

		private void RegisterContentController()
		{
			Ioc.Default.GetRequiredService<CCCMain>()
				.Register();
			Ioc.Default.GetRequiredService<CCCApp>()
				.Register();
		}

		private void RunAppAndMessureTime()
		{
			using (Operation.Time("App loading"))
			{
				try
				{
					Log.Information("Application starting");
					using (IServiceScope scope = _services.CreateScope())
					{
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
		}

		protected override void OnExit(ExitEventArgs e)
		{
			Log.Warning("Application stopped");
			//TODO: Add logline feeature again			LoggingConfigurator.LogLine();
			Log.CloseAndFlush();
		}
	}
}