using ControlzEx.Theming;
using WPF.AppUI.Frame;
using WPF.AppUI.Home;
using WPF.Utilities.ContentController.Services;
using Serilog;
using System.Windows.Controls;

namespace WPF.Main
{
	public class MainWindowModel : ViewModelBase
	{
		public ContentControl CCMain => _mainContent.Control;
		public ContentControl CCHeader { get; }
		public ContentControl CCFooter { get; }
		public string WindowTitle { get => _appInfo.WindowTitle; }

		private readonly IContentControllerService _cc;
		private readonly MainContentStore _mainContent;
		private readonly AppInfo _appInfo;

		public MainWindowModel(AppInfo appInfo, IContentControllerService contentControllerService, MainContentStore mainContentStore)
		{
			_appInfo = appInfo;
			_cc = contentControllerService;

			_mainContent = mainContentStore;
			_mainContent.OnContentChanged += UpdateMainContent;
			_mainContent.Control = _cc.GetUserControl(nameof(HomeView));

			CCHeader = _cc.GetUserControl(nameof(HeaderView));
			CCFooter = _cc.GetUserControl(nameof(FooterView));
			SetTheme();
			Log.Debug("Main window loaded");
		}

		private void UpdateMainContent()
		{
			OnPropertyChanged(nameof(CCMain));
		}

		private void SetTheme()
		{
			ThemeManager.Current.ChangeTheme(App.Current, "Dark.Steel");
			ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncAll;
			Log.Debug("Theme set and sync enabled");
		}
	}
}