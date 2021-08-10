using NetDriveManager.WPF.viewModels;
using System;

namespace NetDriveManager.WPF.utilities.navigation.models
{
	public class NavigationDataModel : INavigationDataModel
	{
		public NavigationDataModel(string name, Func<object> createViewFunc, Func<ViewModelBase> createViewModelFunc)
		{
			Name = name;
			CreateViewFunc = createViewFunc;
			CreateViewModelFunc = createViewModelFunc;
		}

		public string Name { get; }
		public Func<object> CreateViewFunc { get; }
		public Func<ViewModelBase> CreateViewModelFunc { get; }
	}
}