using NetDriveManager.WPF.utilities.contentController.models;
using NetDriveManager.WPF.utilities.contentController.stores;
using NetDriveManager.WPF.viewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NetDriveManager.WPF.utilities.contentController.services
{
	public class ContentControllerService : ContentControllerServiceBase, IContentControllerService
	{
		private readonly IContentStore _store;

		public Window GetWindow(string name) => CreateWindowWithDataContext(name);

		public async Task<Window> GetWindowAndActivateAsync(string name, object parameter = null)
		{
			Window view = CreateWindowWithDataContext(name);
			await ActivateWindow(view, parameter);
			return view;
		}

		public UserControl GetUserControl(string name) => CreateUserControlWithDataContext(name);

		private UserControl CreateUserControlWithDataContext(string name)
		{
			IContentDataModel cdm = _store.GetContentData(nameof(UserControl), name);
			UserControl view = CreateViewOfData(cdm) as UserControl;
			_ = view ?? throw new Exception($"[CC] Failed to create UserControl for Name: { cdm.Name }");
			ViewModelBase viewModel = CreateViewModelOfData(cdm);
			if (viewModel != null)
				view.DataContext = viewModel;

			return view;
		}

		private Window CreateWindowWithDataContext(string name)
		{
			IContentDataModel cdm = _store.GetContentData(nameof(Window), name);
			Window view = CreateViewOfData(cdm) as Window;
			_ = view ?? throw new Exception($"[CC] Failed to create Window for Name: { cdm.Name }");

			ViewModelBase viewModel = CreateViewModelOfData(cdm);
			if (viewModel != null)
				view.DataContext = viewModel;

			return view;
		}

		public ContentControllerService(IContentStore store)
		{
			_store = store;
		}
	}
}