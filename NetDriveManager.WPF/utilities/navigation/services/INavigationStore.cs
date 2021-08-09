using NetDriveManager.WPF.utilities.navigation.models;
using NetDriveManager.WPF.viewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace NetDriveManager.WPF.utilities.navigation.services
{
	public interface INavigationStore
	{
		void AddUserControl(string name, Func<UserControl> createViewFunc, Func<ViewModelBase> createViewModelFunc);
		void AddWindow(string name, Func<Window> createViewFunc, Func<ViewModelBase> createViewModelFunc);
		UserControlNavigationDataModel GetUserControl(string name);
		WindowNavigationDataModel GetWindow(string name);
	}
}