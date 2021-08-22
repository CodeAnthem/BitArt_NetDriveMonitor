using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.Monitor;
using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.helpers;
using NetDriveManager.WPF.utilities.contentController.config;
using NetDriveManager.WPF.utilities.contentController.services;
using NetDriveManager.WPF.utilities.contentController.stores;
using NetDriveManager.WPF.viewModels;
using NetDriveManager.WPF.views;
using Serilog;

namespace NetDriveManager.WPF
{
	public class Services_Config
	{
		private readonly IServiceCollection _services;

		public Services_Config(IServiceCollection services = null)
		{
			_services = services ?? new ServiceCollection();
		}

		public IServiceCollection GetMergedServices()
		{
			AddContentControl();
			AddViewModels();
			AddViews();
			AddLogging();

			_services.AddSingleton<MainContentStore>();

			_services.AddNetdriveManager();

			return _services;
		}

		private void AddContentControl()
		{
			_services.AddSingleton<IContentStore, ContentStore>();
			_services.AddSingleton<IContentControllerService, ContentControllerService>();
			_services.AddSingleton<IControllerConfig, ControllerConfig>();
			//serviceCollection.AddSingleton<IControllerConfig, ControllerConfig>(sc =>
			//	new ControllerConfig(sc, sc.GetRequiredService<IContentStore>()));
		}

		private void AddViewModels()
		{
			_services.AddSingleton<MainWindowViewModel>();
			_services.AddSingleton<HeaderControlViewModel>();
			_services.AddSingleton<FooterPartViewModel>();
			_services.AddSingleton<OverviewViewModel>();
			_services.AddSingleton<SettingsViewModel>();
			_services.AddSingleton<ManageDrivesViewModel>();
		}

		private void AddViews()
		{
			_services.AddTransient<MainWindow>();
			_services.AddTransient<HeaderPartControl>();
			_services.AddTransient<FooterPartControl>();
			_services.AddTransient<OverviewControl>();
			_services.AddTransient<SettingsControl>();
			_services.AddTransient<ManageDrivesControl>();
		}

		private void AddLogging()
		{
			LoggerConfiguration seriCfg = new Serilog_Config().GetSerilogLoggerConfiguration();
			ILogger logger = seriCfg.CreateLogger();
			_services.AddSingleton<ILogger>(logger);
		}
	}
}