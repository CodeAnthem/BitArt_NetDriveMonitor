using ControlzEx.Theming;
using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.utilities.contentController.services;
using NetDriveManager.WPF.views;
using System.Windows.Controls;

namespace NetDriveManager.WPF.viewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		private readonly IContentControllerService _cc;
		public ContentControl CCHeader { get; }
		public ContentControl CCNavStatus { get; }
		public ContentControl CCMain { get; }
		public ContentControl CCFooter { get; }

		public MainWindowViewModel(IContentControllerService contentControllerService)
		{
			_cc = contentControllerService;
			ThemeManager.Current.ChangeTheme(App.Current, "Dark.Steel");
			ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncAll;

			CCHeader = _cc.GetUserControl(nameof(HeaderPartControl));
			CCNavStatus = _cc.GetUserControl(nameof(NavigationStatusPartControl));
			CCMain = _cc.GetUserControl(nameof(OverviewControl));
			CCFooter = _cc.GetUserControl(nameof(FooterPartControl));
		}
	}
}