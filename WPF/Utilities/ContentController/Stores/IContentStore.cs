using WPF.Main;
using WPF.Utilities.ContentController.Models;
using System;

namespace WPF.Utilities.ContentController.Stores
{
	public interface IContentStore
	{
		bool AddUserControl(string name, Func<object> createViewFunc, Func<ViewModelBase> createViewModelFunc = null);

		bool AddWindow(string name, Func<object> createViewFunc, Func<ViewModelBase> createViewModelFunc = null);

		IContentDataModel GetContentData(string type, string name);

		int GetCount();
	}
}