using ControlzEx.Theming;
using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.helpers;
using NetDriveManager.WPF.utilities.contentController.services;
using NetDriveManager.WPF.views;
using Serilog;
using SerilogTimings.Extensions;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Controls;

namespace NetDriveManager.WPF.viewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public ContentControl CCMain => _mainContent.Control;
		public ContentControl CCHeader { get; }
		public ContentControl CCFooter { get; }

		private readonly IContentControllerService _cc;
		private readonly MainContentStore _mainContent;

		public MainWindowViewModel(IContentControllerService contentControllerService, MainContentStore mainContentStore)
		{
			_cc = contentControllerService;

			_mainContent = mainContentStore;
			_mainContent.OnContentChanged += UpdateMainContent;
			_mainContent.Control = _cc.GetUserControl(nameof(ManageDrivesControl));

			CCHeader = _cc.GetUserControl(nameof(HeaderPartControl));
			CCFooter = _cc.GetUserControl(nameof(FooterPartControl));
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