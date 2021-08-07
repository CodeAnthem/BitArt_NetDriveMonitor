using System;
using System.Threading.Tasks;

namespace NetDriveManager.WPF.services
{
	public interface INavigationService
	{
		void Configure(string key, Type windowType);
		Task ShowAsync(string windowKey, object parameter = null);
		Task<bool?> ShowDialogAsync(string windowKey, object parameter = null);
	}
}