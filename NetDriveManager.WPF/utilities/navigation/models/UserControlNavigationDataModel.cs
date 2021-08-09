using NetDriveManager.WPF.viewModels;
using System;
using System.Windows.Controls;

namespace NetDriveManager.WPF.utilities.navigation.models
{
	public class UserControlNavigationDataModel
	{
		public UserControlNavigationDataModel(string name, Func<UserControl> createViewFunc, Func<ViewModelBase> createViewModelFunc)
		{
			Name = name;
			CreateViewFunc = createViewFunc;
			CreateViewModelFunc = createViewModelFunc;
		}

		public string Name { get; }
		public Func<UserControl> CreateViewFunc { get; }
		public Func<ViewModelBase> CreateViewModelFunc { get; }
	}
}