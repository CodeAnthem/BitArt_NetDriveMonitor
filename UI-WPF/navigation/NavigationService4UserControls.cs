using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using UI_WPF.controls;

namespace UI_WPF.navigation
{
	public class NavigationService4UserControls : INavigationService
	{
		private readonly Dictionary<Type, Type> _viewMapping = new Dictionary<Type, Type>
		{
			// List of all pages
			[typeof(MainPageViewModel)] = typeof(MainPage),
			[typeof(SettingsPageViewModel)] = typeof(SettingsPage),
		};

		public object Navigate<T>()
		{
			return (_viewMapping[typeof(T)]);
		}
	}
}