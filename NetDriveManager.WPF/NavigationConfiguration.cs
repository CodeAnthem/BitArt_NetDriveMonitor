using NetDriveManager.WPF.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveManager.WPF
{
	public static class NavigationConfiguration
	{
		public static void Configure(INavigationService navigationService)
		{
			navigationService.Configure(nameof(MainWindow), typeof(MainWindow));
		}
	}
}