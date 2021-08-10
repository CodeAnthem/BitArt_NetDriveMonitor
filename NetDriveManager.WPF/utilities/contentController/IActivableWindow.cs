using System.Threading.Tasks;

namespace NetDriveManager.WPF.utilities.contentController
{
	public interface IActivableWindow
	{
		Task ActivateAsync(object parameter);
	}
}