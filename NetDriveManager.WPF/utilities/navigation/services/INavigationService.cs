using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NetDriveManager.WPF.utilities.navigation.services
{
	public interface INavigationService
	{
		UserControl GetUserControlWithDataContext(string name, object parameter = null);

		Task<Window> GetWindowWithDataContextAsync(string name, object parameter = null);

		Task ShowWindowAsync(string name, object parameter = null);

		Task<bool?> ShowWindowModalAsync(string name, object parameter = null);
	}
}