using NetDriveManager.WPF.Main;
using NetDriveManager.WPF.utilities.contentController.models;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace NetDriveManager.WPF.utilities.contentController.stores
{
	public class ContentStoreBase
	{
		internal object CreateView(IContentDataModel ndm) => ndm.CreateViewFunc();

		internal ViewModelBase CreatetViewModel(IContentDataModel ndm) => ndm.CreateViewModelFunc();

		internal async Task<Window> CreateAndActivateWindowAsync(IContentDataModel ndm, object parameter = null)
		{
			Window window = ndm.CreateViewFunc() as Window;

			if (window.DataContext is IActivableWindow activable)
			{
				await activable.ActivateAsync(parameter);
			}

			return window;
		}

		internal IContentDataModel ValidateAndCreateContentData(string name, Func<object> createViewFunc, Func<ViewModelBase> createViewModelFunc)
		{
			ThrowIfStringEmpty(name);
			_ = createViewFunc ?? throw new Exception($"[CC] Missing required func to create view. Name: {name}");
			return new ContentDataModel(name, createViewFunc, createViewModelFunc);
		}

		internal static void ThrowIfStringEmpty(string name)
		{
			bool isEmpty = string.IsNullOrWhiteSpace(name);
			_ = !isEmpty ? true : throw new Exception("[CC] Content name cannot be empty.");
		}
	}
}