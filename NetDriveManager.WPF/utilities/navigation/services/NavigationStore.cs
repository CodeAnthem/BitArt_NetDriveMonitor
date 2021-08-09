using NetDriveManager.WPF.utilities.navigation.models;
using NetDriveManager.WPF.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace NetDriveManager.WPF.utilities.navigation.services
{
	public class NavigationStore : INavigationStore
	{
		private List<WindowNavigationDataModel> WindowsList { get; } = new List<WindowNavigationDataModel>();
		private List<UserControlNavigationDataModel> UserControlsList { get; } = new List<UserControlNavigationDataModel>();

		public void AddWindow(string name, Func<Window> createViewFunc, Func<ViewModelBase> createViewModelFunc)
		{
			InputValidation(name, createViewFunc, createViewModelFunc);
			var newWindow = new WindowNavigationDataModel(name, createViewFunc, createViewModelFunc);
			WindowsList.Add(newWindow);
		}

		public void AddUserControl(string name, Func<UserControl> createViewFunc, Func<ViewModelBase> createViewModelFunc)
		{
			InputValidation(name, createViewFunc, createViewModelFunc);
			var newUserControl = new UserControlNavigationDataModel(name, createViewFunc, createViewModelFunc);
			UserControlsList.Add(newUserControl);
		}

		public WindowNavigationDataModel GetWindow(string name) => WindowsList.First(x => x.Name == name);

		public UserControlNavigationDataModel GetUserControl(string name) => UserControlsList.First(x => x.Name == name);

		private void InputValidation(string name, object createViewFunc, object createViewModelFunc)
		{
			ThrowIfStringEmpty(name);
			_ = createViewFunc ?? throw new ArgumentNullException("View cannot be null", nameof(createViewFunc));
			_ = createViewModelFunc ?? throw new ArgumentNullException("ViewModel cannot be null", nameof(createViewModelFunc));
		}

		private static void ThrowIfStringEmpty(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Name cannot be empty");
			}
		}
	}
}