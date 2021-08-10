using NetDriveManager.WPF.utilities.contentController.models;
using NetDriveManager.WPF.utilities.navigation;
using NetDriveManager.WPF.utilities.navigation.models;
using NetDriveManager.WPF.viewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace NetDriveManager.WPF.utilities.contentController.stores
{
	public class ContentStoreBase
	{
		internal object CreateView(IContentDataModel ndm) => ndm.CreateViewFunc();

		internal ViewModelBase CreatetViewModel(IContentDataModel ndm) => ndm.CreateViewModelFunc();

		internal async Task<Window> CreateAndActivateWindowAsync(IContentDataModel ndm, object parameter = null)
		{
			Window window = CreateView(ndm) as Window;

			if (window.DataContext is IActivableWindow activable)
			{
				await activable.ActivateAsync(parameter);
			}

			return window;
		}

		internal IContentDataModel ValidateAndCreateContentData(string name, Func<object> createViewFunc, Func<ViewModelBase> createViewModelFunc)
		{
			ThrowIfStringEmpty(name);
			_ = createViewFunc ?? throw new ArgumentNullException("View cannot be null", nameof(createViewFunc));
			//_ = createViewModelFunc ?? throw new ArgumentNullException("ViewModel cannot be null", nameof(createViewModelFunc));

			return new ContentDataModel(name, createViewFunc, createViewModelFunc);
		}

		internal static void ThrowIfStringEmpty(string name)
		{
			bool isEmpty = string.IsNullOrWhiteSpace(name);
			_ = !isEmpty ? true : throw new ArgumentException("Name cannot be empty");
		}
	}
}