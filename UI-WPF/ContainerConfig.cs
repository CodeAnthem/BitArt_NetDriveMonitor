using Autofac;
using System.Windows.Controls;
using UI_WPF.controls;
using UI_WPF.navigation;
using UI_WPF.viewmodels;
using UI_WPF.views;

namespace UI_WPF
{
	public static class ContainerConfig
	{
		public static IContainer Configure(ILifetimeScope scope)
		{
			var builder = new ContainerBuilder();

			// navigation
			//var nav = new NavigationService4UserControls(scope);
			//builder.RegisterInstance(nav).As<INavigationService>().SingleInstance();

			// main window
			builder.RegisterType<MainView>();
			builder.RegisterType<MainViewModel>();
			builder.RegisterType<MainPage>();
			builder.RegisterType<MainPageViewModel>();
			builder.RegisterType<SettingsPage>();
			builder.RegisterType<SettingsPageViewModel>();

			// Add stuff here

			return builder.Build();
		}
	}
}