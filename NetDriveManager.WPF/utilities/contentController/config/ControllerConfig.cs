﻿using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.utilities.contentController.stores;
using NetDriveManager.WPF.viewModels;
using NetDriveManager.WPF.views;
using System;

namespace NetDriveManager.WPF.utilities.contentController.config
{
	public class ControllerConfig : IControllerConfig
	{
		private readonly IServiceProvider _ioc;
		private readonly IContentStore _contentStore;

		public void RegisterAll()
		{
			RegisterWindows();
			RegisterUserControls();
		}

		private void RegisterWindows()
		{
			_contentStore.AddWindow(nameof(MainWindow),
				() => _ioc.GetRequiredService<MainWindow>(),
				() => _ioc.GetRequiredService<MainWindowViewModel>()
				);
		}

		private void RegisterUserControls()
		{
			_contentStore.AddUserControl(nameof(HeaderPartControl),
				() => _ioc.GetRequiredService<HeaderPartControl>(),
				() => _ioc.GetRequiredService<HeaderControlViewModel>()
				);

			_contentStore.AddUserControl(nameof(FooterPartControl),
				() => _ioc.GetRequiredService<FooterPartControl>(),
				() => _ioc.GetRequiredService<FooterPartViewModel>()
				);

			_contentStore.AddUserControl(nameof(OverviewControl),
				() => _ioc.GetRequiredService<OverviewControl>(),
				() => _ioc.GetRequiredService<OverviewViewModel>()
				);

			_contentStore.AddUserControl(nameof(SettingsControl),
				() => _ioc.GetRequiredService<SettingsControl>(),
				() => _ioc.GetRequiredService<SettingsViewModel>()
				);

			_contentStore.AddUserControl(nameof(ManageDrivesControl),
				() => _ioc.GetRequiredService<ManageDrivesControl>(),
				() => _ioc.GetRequiredService<ManageDrivesViewModel>()
				);
		}

		public ControllerConfig(IServiceProvider ioc, IContentStore contentStore)
		{
			_ioc = ioc;
			_contentStore = contentStore;
		}
	}
}