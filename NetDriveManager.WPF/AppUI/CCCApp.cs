using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.WPF.AppUI.EditDrives;
using NetDriveManager.WPF.AppUI.Frame;
using NetDriveManager.WPF.AppUI.Home;
using NetDriveManager.WPF.utilities.contentController.stores;
using System;

namespace NetDriveManager.WPF.AppUI
{
	public class CCCApp
	{
		private readonly IServiceProvider _ioc;
		private readonly IContentStore _contentStore;

		public CCCApp(IServiceProvider ioc, IContentStore contentStore)
		{
			_ioc = ioc;
			_contentStore = contentStore;
		}

		public void Register()
		{
			RegisterFrame();
			RegisterHome();
			RegisterEditDrives();
			RegisterSettings();
		}

		private void RegisterFrame()
		{
			_contentStore.AddUserControl(nameof(HeaderView),
				() => _ioc.GetRequiredService<HeaderView>(),
				() => _ioc.GetRequiredService<HeaderViewModel>()
				);

			_contentStore.AddUserControl(nameof(FooterView),
				() => _ioc.GetRequiredService<FooterView>(),
				() => _ioc.GetRequiredService<FooterViewModel>()
				);
		}

		private void RegisterHome()
		{
			_contentStore.AddUserControl(nameof(HomeView),
	() => _ioc.GetRequiredService<HomeView>(),
	() => _ioc.GetRequiredService<HomeViewModel>()
				);
		}

		private void RegisterEditDrives()
		{
			_contentStore.AddUserControl(nameof(EditDrivesView),
() => _ioc.GetRequiredService<EditDrivesView>(),
() => _ioc.GetRequiredService<EditDrivesViewModel>()
	);
		}

		private void RegisterSettings()
		{
		}
	}
}