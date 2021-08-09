using NetDriveManager.WPF.viewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NetDriveManager.WPF.utilities.navigation
{
	public interface INavigationService
	{
		void Add(string name, Func<ContentControl> createView, Func<ViewModelBase> createViewModel);

		Task ShowAsync(string name, object parameter = null);

		Task<bool?> ShowModalAsync(string name, object parameter = null);

		ContentControl Get(string name);
	}
}