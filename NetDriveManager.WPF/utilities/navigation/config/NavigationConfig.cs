using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.utilities.navigation.services;
using NetDriveManager.WPF.viewModels;
using NetDriveManager.WPF.views;
using System;

namespace NetDriveManager.WPF.utilities.navigation.config
{
	public class NavigationConfig
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly INavigationStore _navStore;

		public NavigationConfig(IServiceProvider serviceProvider, INavigationStore navStore)
		{
			_serviceProvider = serviceProvider;
			_navStore = navStore;
			Register();
		}

		private void Register()
		{
			_navStore.AddWindow(nameof(MainWindow),
				() => _serviceProvider.GetRequiredService<MainWindow>(),
				() => _serviceProvider.GetRequiredService<MainWindowViewModel>()
				);

			_navStore.AddUserControl(nameof(HeaderControl),
				() => _serviceProvider.GetRequiredService<HeaderControl>(),
				() => _serviceProvider.GetRequiredService<HeaderControlViewModel>()
				);

			_navStore.AddWindow(nameof(SettingsWindow),
				() => _serviceProvider.GetRequiredService<SettingsWindow>(),
				() => _serviceProvider.GetRequiredService<SettingsViewModel>()
				 );
		}
	}
}