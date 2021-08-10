using NetDriveManager.WPF.viewModels;
using System;

namespace NetDriveManager.WPF.utilities.navigation.models
{
	public interface INavigationDataModel
	{
		Func<object> CreateViewFunc { get; }
		Func<ViewModelBase> CreateViewModelFunc { get; }
		string Name { get; }
	}
}