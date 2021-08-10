using Microsoft.Extensions.DependencyInjection;
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

			_contentStore.AddWindow(nameof(SettingsWindow),
				() => _ioc.GetRequiredService<SettingsWindow>(),
				() => _ioc.GetRequiredService<SettingsViewModel>()
				 );
		}

		private void RegisterUserControls()
		{
			_contentStore.AddUserControl(nameof(HeaderControl),
				() => _ioc.GetRequiredService<HeaderControl>(),
				() => _ioc.GetRequiredService<HeaderControlViewModel>()
				);
		}

		public ControllerConfig(IServiceProvider ioc, IContentStore contentStore)
		{
			_ioc = ioc;
			_contentStore = contentStore;
		}
	}
}