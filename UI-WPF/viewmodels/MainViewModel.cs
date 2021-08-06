using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using UI_WPF.controls;
using UI_WPF.views;

namespace UI_WPF.viewmodels
{
	public class MainViewModel : ViewModelBase
	{
		private UserControl _content;
		public UserControl Content { get => _content; set => SetProperty(ref _content, value); }

		public string MyStringProperty { get; set; }

		public MainViewModel()
		{
			Content = new MainScreen();
			MyStringProperty = "hehehahaha";
		}
	}
}