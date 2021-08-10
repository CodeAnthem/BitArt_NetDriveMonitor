using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NetDriveManager.WPF.utilities.contentController.services
{
	public interface IContentControllerService
	{
		UserControl GetUserControl(string name);
		Window GetWindow(string name);
		Task<Window> GetWindowAndActivateAsync(string name, object parameter = null);
	}
}