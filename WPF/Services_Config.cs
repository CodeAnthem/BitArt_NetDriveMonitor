using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.Monitor;
using WPF.Main;
using Serilog;
using WPF.AppUI;
using WPF.Utilities.ContentController;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace WPF
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
			_services.AddTransient<AppInfo>();

			_services.Add_ContentController();
			_services.Add_UI_Main();

			_services.AddNetdriveManager();
			_services.Add_UI_App();

			_services.Add_SeriLogging();

			return _services;
		}
	}
}