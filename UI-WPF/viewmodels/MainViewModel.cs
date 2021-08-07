using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using UI_WPF.controls;
using UI_WPF.navigation;
using UI_WPF.views;

namespace UI_WPF.viewmodels
{
	public class MainViewModel : ViewModelBase
	{
		public INavigationService _nav;

		private UserControl _content;
		public UserControl Content { get => _content; set => SetProperty(ref _content, value); }

		public string MyStringProperty { get; set; } = "hehehahaha";

		public MainViewModel(INavigationService nav)
		{
			_nav = nav;
			var uc = _nav.Navigate<MainPage>() as UserControl;
			Content = uc;
		}
	}
}