using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.viewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetDriveManager.WPF.utilities.navigation
{
	public class NavigationConfig : INavigationConfig
	{
		#region Private Fields

		private IServiceProvider _serviceProvider;
		private INavigationService _navigationService;

		#endregion Private Fields

		#region Public Constructors

		public NavigationConfig(IServiceProvider serviceProvider, INavigationService navigationService)
		{
			_serviceProvider = serviceProvider;
			_navigationService = navigationService;
			Register();
		}

		#endregion Public Constructors

		#region Public Methods

		private void Register()
		{
			_navigationService.Add(nameof(MainWindow),
				() => _serviceProvider.GetRequiredService<MainWindow>(),
				() => _serviceProvider.GetRequiredService<MainWindowViewModel>()
				);

			_navigationService.Add(nameof(HeaderControl),
				() => _serviceProvider.GetRequiredService<HeaderControl>(),
				() => _serviceProvider.GetRequiredService<HeaderControlViewModel>()
				);
		}

		#endregion Public Methods
	}
}