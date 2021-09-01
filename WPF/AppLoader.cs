using ControlzEx.Theming;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Serilog;
using SerilogTimings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WPF.AppUI;
using WPF.Main;
using WPF.Utilities.ContentController.Services;

namespace WPF
{
	public class AppLoader
	{
		private readonly IServiceProvider _services;

		public AppLoader()
		{
			// SETUP DI CONTAINER
			_services = new Services_Config().GetMergedServices()
				.BuildServiceProvider();
			Ioc.Default.ConfigureServices(_services);
		}

		public void Init()
		{
			SetupGlobalLogger();
			RegisterContentController();
			RunAppAndMessureTime();
		}

		public void Exit(ExitEventArgs e)
		{
			windowSettings.Default.Save();
			Log.Information($"Application stopped with code: {e.ApplicationExitCode}");
			//TODO: Add logline feeature again			LoggingConfigurator.LogLine();
			Log.CloseAndFlush();
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
			Log.Debug("Content Controller Registered");
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
	}
}