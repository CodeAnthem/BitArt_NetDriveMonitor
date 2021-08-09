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
		internal async Task<Window> GetAndActivateWindowAsync(WindowNavigationDataModel ndm, object parameter = null)
		{
			var window = GetView(ndm);

			if (window.DataContext is IActivable activable)
			{
				await activable.ActivateAsync(parameter);
			}

			return window as Window;
		}

		internal Window GetView(WindowNavigationDataModel ndm)
		{
			return ndm.CreateViewFunc();
		}

		internal UserControl GetView(UserControlNavigationDataModel ndm)
		{
			return ndm.CreateViewFunc();
		}

		internal ViewModelBase GetViewModel(WindowNavigationDataModel ndm)
		{
			return ndm.CreateViewModelFunc() as ViewModelBase;
		}

		internal ViewModelBase GetViewModel(UserControlNavigationDataModel ndm)
		{
			return ndm.CreateViewModelFunc() as ViewModelBase;
		}
	}
}