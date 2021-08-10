using NetDriveManager.WPF.utilities.navigation.models;
using NetDriveManager.WPF.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetDriveManager.WPF.utilities.navigation.services
{
	public class NavigationStore : INavigationStore
	{
		private List<NavigationDataModel> WindowsList { get; } = new List<NavigationDataModel>();
		private List<NavigationDataModel> UserControlsList { get; } = new List<NavigationDataModel>();

		public void AddWindow(string name, Func<object> createViewFunc, Func<ViewModelBase> createViewModelFunc)
		{
			InputValidation(name, createViewFunc, createViewModelFunc);
			var newWindow = new NavigationDataModel(name, createViewFunc, createViewModelFunc);
			WindowsList.Add(newWindow);
		}

		public void AddUserControl(string name, Func<object> createViewFunc, Func<ViewModelBase> createViewModelFunc)
		{
			InputValidation(name, createViewFunc, createViewModelFunc);
			var newUserControl = new NavigationDataModel(name, createViewFunc, createViewModelFunc);
			UserControlsList.Add(newUserControl);
		}

		public NavigationDataModel GetWindowNavigationData(string name) => WindowsList.FirstOrDefault(x => x.Name == name);

		public NavigationDataModel GetUserControlNavigationData(string name) => UserControlsList.FirstOrDefault(x => x.Name == name);

		private void InputValidation(string name, object createViewFunc, object createViewModelFunc)
		{
			//ThrowIfStringEmpty(name);
			_ = createViewFunc ?? throw new ArgumentNullException("View cannot be null", nameof(createViewFunc));
			_ = createViewModelFunc ?? throw new ArgumentNullException("ViewModel cannot be null", nameof(createViewModelFunc));
		}
	}
}