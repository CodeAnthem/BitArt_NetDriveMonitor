using Microsoft.Extensions.DependencyInjection;
using WPF.Utilities.ContentController.Stores;
using System;

namespace WPF.Main
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
			_contentStore.AddWindow(nameof(MainWindow),
				() => _ioc.GetRequiredService<MainWindow>(),
				() => _ioc.GetRequiredService<MainWindowModel>()
				);
		}
	}
}