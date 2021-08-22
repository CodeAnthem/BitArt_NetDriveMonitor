using NetDriveManager.WPF.Main;
using System;

namespace NetDriveManager.WPF.utilities.contentController.models
{
	public interface IContentDataModel
	{
		Func<object> CreateViewFunc { get; }
		Func<ViewModelBase> CreateViewModelFunc { get; }
		string Name { get; }
	}
}