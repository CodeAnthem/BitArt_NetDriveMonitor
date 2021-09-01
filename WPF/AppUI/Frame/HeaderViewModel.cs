using ControlzEx.Theming;
using MaterialDesignThemes.Wpf;
using Microsoft.Toolkit.Mvvm.Input;
using Serilog;
using System.Windows;
using System.Windows.Input;
using WPF.Main;

namespace WPF.AppUI.Frame
{
	public class HeaderViewModel : ViewModelBase
	{
		public string HeaderTitle { get => _appInfo.HeaderTitle; }
		public string HeaderSubTitle { get => _appInfo.HeaderSubTitle; }
		public ICommand ChangeThemeDark { get; }
		public ICommand ChangeThemeLight { get; }
		public ICommand ToggleBaseThemeCommand { get; }


		private readonly AppInfo _appInfo;
		private bool isDarkTheme;

		public bool IsDarkTheme
		{
			get { return isDarkTheme; }
			set { SetProperty(ref isDarkTheme, value); }
		}

		public HeaderViewModel(AppInfo appInfo)
		{
			_appInfo = appInfo;
			CheckCurrentBaseTheme();
			ChangeThemeDark = new RelayCommand(ChangeBaseThemeDark);
			ChangeThemeLight = new RelayCommand(ChangeBaseThemeLight);
			ToggleBaseThemeCommand = new RelayCommand(ToggleBaseTheme);

			ThemeManager.Current.ThemeChanged += Current_ThemeChanged;
			ThemeManager.Current.ChangeThemeBaseColor(Application.Current, "Dark");
			//ThemeManager.Current.Themes = "Dark";
		}

		private void Current_ThemeChanged(object sender, ControlzEx.Theming.ThemeChangedEventArgs e)
		{
			CheckCurrentBaseTheme();
		}

		private void CheckCurrentBaseTheme()
		{
			var currentBase = ThemeManager.Current.DetectTheme(Application.Current);
			if (currentBase.BaseColorScheme == "Dark")
			{
				IsDarkTheme = true;
				return;
			}
			IsDarkTheme = false;
		}

		public void ToggleBaseTheme()
		{
			if (IsDarkTheme)
			{
				ChangeBaseThemeDark();
				return;
			}
			ChangeBaseThemeLight();
		}

		public void ChangeBaseThemeDark()
		{
			ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAccent;
			ThemeManager.Current.ChangeThemeBaseColor(Application.Current, "Dark");
			Log.Debug("Changed base theme to dark via toggle");
			ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncAll;

		}

		public void ChangeBaseThemeLight()
		{
			ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAccent;
			ThemeManager.Current.ChangeThemeBaseColor(Application.Current, "Light");
			Log.Debug("Changed base theme to light via toggle");
			ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncAll;


		}
	}
}