using NetDriveManager.WPF.utilities.navigation.models;
using NetDriveManager.WPF.viewModels;
using System;

namespace NetDriveManager.WPF.utilities.navigation.services
{
	public interface INavigationStore
	{
		void AddUserControl(string name, Func<object> createViewFunc, Func<ViewModelBase> createViewModelFunc);

		void AddWindow(string name, Func<object> createViewFunc, Func<ViewModelBase> createViewModelFunc);

		NavigationDataModel GetUserControlNavigationData(string name);

		NavigationDataModel GetWindowNavigationData(string name);
	}
}