using NetDriveManager.WPF.viewModels;
using System;
using System.Windows;

namespace NetDriveManager.WPF.utilities.navigation.models
{
	public class WindowNavigationDataModel
	{
		public WindowNavigationDataModel(string name, Func<Window> createViewFunc, Func<ViewModelBase> createViewModelFunc)
		{
			Name = name;
			CreateViewFunc = createViewFunc;
			CreateViewModelFunc = createViewModelFunc;
		}

		public string Name { get; }
		public Func<Window> CreateViewFunc { get; }
		public Func<ViewModelBase> CreateViewModelFunc { get; }
	}
}