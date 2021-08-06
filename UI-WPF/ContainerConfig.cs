using Autofac;
using UI_WPF.controls;
using UI_WPF.viewmodels;
using UI_WPF.views;

namespace UI_WPF
{
	public static class ContainerConfig
	{
		public static IContainer Configure()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<MainScreen>();

			// Add stuff here
			builder.RegisterType<MainView>();
			builder.RegisterType<MainViewModel>();

			return builder.Build();
		}
	}
}