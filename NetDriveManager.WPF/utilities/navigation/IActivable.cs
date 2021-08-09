using System.Threading.Tasks;

namespace NetDriveManager.WPF.utilities.navigation
{
	public interface IActivable
	{
		Task ActivateAsync(object parameter);
	}
}