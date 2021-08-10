using NetDriveManager.WPF.utilities.contentController.models;
using NetDriveManager.WPF.viewModels;
using System.Threading.Tasks;
using System.Windows;

namespace NetDriveManager.WPF.utilities.contentController.services
{
	public class ContentControllerServiceBase
	{
		internal object CreateViewOfData(IContentDataModel contentDataModel) => contentDataModel.CreateViewFunc();

		internal ViewModelBase CreateViewModelOfData(IContentDataModel contentDataModel) => contentDataModel.CreateViewModelFunc();

		internal async Task ActivateWindow(Window window, object parameter)
		{
			if (window.DataContext is IActivableWindow activable)
			{
				await activable.ActivateAsync(parameter);
			}
		}
	}
}