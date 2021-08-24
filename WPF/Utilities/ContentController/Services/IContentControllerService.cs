using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF.Utilities.ContentController.Services
{
	public interface IContentControllerService
	{
		UserControl GetUserControl(string name);

		Window GetWindow(string name);

		Task<Window> GetWindowAndActivateAsync(string name, object parameter = null);
	}
}