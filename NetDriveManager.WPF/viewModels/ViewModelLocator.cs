using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace NetDriveManager.WPF.viewModels
{
	public class ViewModelLocator
	{
		public MainWindowViewModel MainWindowViewModel { get => Ioc.Default.GetRequiredService<MainWindowViewModel>(); }
	}
}