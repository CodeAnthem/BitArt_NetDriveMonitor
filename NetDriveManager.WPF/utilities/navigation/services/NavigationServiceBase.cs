using NetDriveManager.WPF.utilities.navigation.models;
using NetDriveManager.WPF.viewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NetDriveManager.WPF.utilities.navigation.services
{
	public class NavigationServiceBase
	{
		internal async Task<Window> GetAndActivateWindowAsync(NavigationDataModel ndm, object parameter = null)
		{
			var window = GetView(ndm) as Window;

			if (window.DataContext is IActivable activable)
			{
				await activable.ActivateAsync(parameter);
			}

			return window;
		}

		internal object GetView(NavigationDataModel ndm) => ndm.CreateViewFunc();

		internal ViewModelBase GetViewModel(NavigationDataModel ndm) => ndm.CreateViewModelFunc();
	}
}