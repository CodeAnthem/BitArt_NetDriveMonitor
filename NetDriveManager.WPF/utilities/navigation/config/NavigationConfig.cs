﻿using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.utilities.navigation.services;
using NetDriveManager.WPF.viewModels;
using NetDriveManager.WPF.views;
using System;

namespace NetDriveManager.WPF.utilities.navigation.config
{
	public class NavigationConfig : INavigationConfig
	{
		private readonly IServiceProvider _ioc;
		private readonly INavigationStore _navStore;

		public NavigationConfig(IServiceProvider serviceProvider, INavigationStore navigationStore)
		{
			_ioc = serviceProvider;
			_navStore = navigationStore;
		}

		public void RegisterAll()
		{
			RegisterWindows();
			RegisterUserControls();
		}

		private void RegisterWindows()
		{
			_navStore.AddWindow(nameof(MainWindow),
				() => _ioc.GetRequiredService<MainWindow>(),
				() => _ioc.GetRequiredService<MainWindowViewModel>()
				);

			_navStore.AddWindow(nameof(SettingsWindow),
				() => _ioc.GetRequiredService<SettingsWindow>(),
				() => _ioc.GetRequiredService<SettingsViewModel>()
				 );
		}

		private void RegisterUserControls()
		{
			_navStore.AddUserControl(nameof(HeaderControl),
				() => _ioc.GetRequiredService<HeaderControl>(),
				() => _ioc.GetRequiredService<HeaderControlViewModel>()
				);
		}
	}
}