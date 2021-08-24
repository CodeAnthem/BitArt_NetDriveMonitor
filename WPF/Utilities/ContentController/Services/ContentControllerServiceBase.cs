using WPF.Main;
using WPF.Utilities.ContentController.Models;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.Utilities.ContentController.Services
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