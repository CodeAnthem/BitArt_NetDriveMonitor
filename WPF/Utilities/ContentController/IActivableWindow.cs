using System.Threading.Tasks;

namespace WPF.Utilities.ContentController
{
	public interface IActivableWindow
	{
		Task ActivateAsync(object parameter);
	}
}