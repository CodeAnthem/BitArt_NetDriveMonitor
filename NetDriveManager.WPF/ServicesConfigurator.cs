﻿using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.Monitor;
using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.helpers;
using NetDriveManager.WPF.utilities.contentController.config;
using NetDriveManager.WPF.utilities.contentController.services;
using NetDriveManager.WPF.utilities.contentController.stores;
using NetDriveManager.WPF.viewModels;
using NetDriveManager.WPF.views;

namespace NetDriveManager.WPF
{
	public static class ServicesConfigurator
	{
		public static IServiceCollection GetServices()
		{
			var serviceCollection = new ServiceCollection();

			#region Content Control

			serviceCollection.AddSingleton<IContentStore, ContentStore>();
			serviceCollection.AddSingleton<IContentControllerService, ContentControllerService>();
			serviceCollection.AddSingleton<IControllerConfig, ControllerConfig>();
			//serviceCollection.AddSingleton<IControllerConfig, ControllerConfig>(sc =>
			//	new ControllerConfig(sc, sc.GetRequiredService<IContentStore>()));

			#endregion Content Control

			#region View Models

			serviceCollection.AddSingleton<MainWindowViewModel>();

			serviceCollection.AddSingleton<HeaderControlViewModel>();
			serviceCollection.AddSingleton<FooterPartViewModel>();

			serviceCollection.AddSingleton<OverviewViewModel>();
			serviceCollection.AddSingleton<SettingsViewModel>();
			serviceCollection.AddSingleton<ManageDrivesViewModel>();

			#endregion View Models

			#region Views

			serviceCollection.AddTransient<MainWindow>();

			serviceCollection.AddTransient<HeaderPartControl>();
			serviceCollection.AddTransient<FooterPartControl>();

			serviceCollection.AddTransient<OverviewControl>();
			serviceCollection.AddTransient<SettingsControl>();
			serviceCollection.AddTransient<ManageDrivesControl>();

			#endregion Views

			serviceCollection.AddSingleton<MainContentStore>();

			#region Netdrive Monitor

			serviceCollection.AddSingleton<INetdriveMonitor, NetdriveMonitor>();

			#endregion Netdrive Monitor

			return serviceCollection;
		}
	}
}