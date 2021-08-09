using NetDriveManager.WPF.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NetDriveManager.WPF.utilities.navigation
{
	public class NavigationService : INavigationService
	{
		#region Private Properties

		private List<NavigationDataModel> Content { get; } = new List<NavigationDataModel>();

		#endregion Private Properties

		#region Public Methods

		public void Add(string name, Func<ContentControl> createViewFunc, Func<ViewModelBase> createViewModelFunc)
		{
			CheckValidName(name);
			_ = createViewFunc ?? throw new ArgumentNullException("View cannot be null", nameof(createViewFunc));
			_ = createViewModelFunc ?? throw new ArgumentNullException("ViewModel cannot be null", nameof(createViewModelFunc));

			var newContent = new NavigationDataModel(name, createViewFunc, createViewModelFunc);
			Content.Add(newContent);
		}

		public async Task ShowAsync(string name, object parameter = null)
		{
			NavigationDataModel ndm = GetNavigationDataModel(name);
			Window view = await GetAndActivateWindowAsync(ndm, parameter);
			ViewModelBase viewModel = GetViewModel(ndm);

			view.DataContext = viewModel;
			view.Show();
		}

		public ContentControl Get(string name)
		{
			NavigationDataModel ndm = GetNavigationDataModel(name);
			ContentControl view = GetView(ndm);
			ViewModelBase viewModel = GetViewModel(ndm);

			view.DataContext = viewModel;
			return view;
		}

		public async Task<bool?> ShowModalAsync(string name, object parameter = null)
		{
			NavigationDataModel ndm = GetNavigationDataModel(name);
			Window view = await GetAndActivateWindowAsync(ndm, parameter);
			ViewModelBase viewModel = GetViewModel(ndm);

			view.DataContext = viewModel;
			return view.ShowDialog();
		}

		#endregion Public Methods

		#region Private Methods

		private static void CheckValidName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Name cannot be empty");
			}
		}

		private ContentControl GetView(NavigationDataModel ndm)
		{
			return ndm.View();
		}

		private ViewModelBase GetViewModel(NavigationDataModel ndm)
		{
			return ndm.ViewModel() as ViewModelBase;
		}

		private async Task<Window> GetAndActivateWindowAsync(NavigationDataModel ndm, object parameter = null)
		{
			var window = GetView(ndm);

			if (window.DataContext is IActivable activable)
			{
				await activable.ActivateAsync(parameter);
			}

			return window as Window;
		}

		private NavigationDataModel GetNavigationDataModel(string name)
		{
			return Content.First(x => x.NameOf == name);
		}

		#endregion Private Methods
	}
}