using NetDriveManager.WPF.utilities.contentController.models;
using NetDriveManager.WPF.viewModels;
using System;

namespace NetDriveManager.WPF.utilities.contentController.stores
{
	public interface IContentStore
	{
		bool AddUserControl(string name, Func<object> createViewFunc, Func<ViewModelBase> createViewModelFunc = null);
		bool AddWindow(string name, Func<object> createViewFunc, Func<ViewModelBase> createViewModelFunc = null);
		IContentDataModel GetContentData(string type, string name);
	}
}