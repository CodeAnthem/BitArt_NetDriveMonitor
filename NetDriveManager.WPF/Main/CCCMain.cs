using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.WPF.utilities.contentController.stores;
using System;

namespace NetDriveManager.WPF.Main
{
	public class CCCMain
	{
		private readonly IServiceProvider _ioc;
		private readonly IContentStore _contentStore;

		public CCCMain(IServiceProvider ioc, IContentStore contentStore)
		{
			_ioc = ioc;
			_contentStore = contentStore;
		}

		public void Register()
		{
			_contentStore.AddUserControl(nameof(MainWindow),
				() => _ioc.GetRequiredService<MainWindow>(),
				() => _ioc.GetRequiredService<MainWindowModel>()
				);
		}
	}
}