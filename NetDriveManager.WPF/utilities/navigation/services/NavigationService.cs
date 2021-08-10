using NetDriveManager.WPF.utilities.navigation.models;
using NetDriveManager.WPF.viewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NetDriveManager.WPF.utilities.navigation.services
{
	public class NavigationService : NavigationServiceBase, INavigationService
	{
		private readonly INavigationStore _navStore;

		public NavigationService(INavigationStore navStore)
		{
			_navStore = navStore;
		}

		public async Task<Window> GetWindowWithDataContextAsync(string name, object parameter = null) =>
			await CreateWindowWithDataContext(name, parameter);

		public async Task ShowWindowAsync(string name, object parameter = null)
		{
			Window view = await CreateWindowWithDataContext(name, parameter);
			view.Show();
		}

		public async Task<bool?> ShowWindowModalAsync(string name, object parameter = null)
		{
			Window view = await CreateWindowWithDataContext(name, parameter);
			return view.ShowDialog();
		}

		private async Task<Window> CreateWindowWithDataContext(string name, object parameter)
		{
			var navData = _navStore.GetWindowNavigationData(name);
			_ = navData ?? throw new NullReferenceException($"No window registered with the name: {name}");

			Window view = await GetAndActivateWindowAsync(navData, parameter);
			ViewModelBase viewModel = GetViewModel(navData);
			view.DataContext = viewModel;
			return view;
		}

		public UserControl GetUserControlWithDataContext(string name, object parameter = null) => CreateUserControlWithDataContext(name, parameter);

		private UserControl CreateUserControlWithDataContext(string name, object parameter)
		{
			var navData = _navStore.GetUserControlNavigationData(name);
			_ = navData ?? throw new NullReferenceException($"No user control registered with the name: {name}");

			UserControl view = GetView(navData) as UserControl;
			ViewModelBase viewModel = GetViewModel(navData);
			view.DataContext = viewModel;
			return view;
		}
	}
}