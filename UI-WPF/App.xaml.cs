using Autofac;
using System.Windows;
using UI_WPF.viewmodels;
using UI_WPF.views;

namespace UI_WPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly IContainer _container;

		protected override void OnStartup(StartupEventArgs e)
		{
			using (var scope = _container.BeginLifetimeScope())
			{
				var view = scope.Resolve<MainView>();
				var viewModel = scope.Resolve<MainViewModel>();
				view.DataContext = viewModel;
				view.Show();
				base.OnStartup(e);
			}
		}

		public App()
		{
			_container = ContainerConfig.Configure();
		}
	}
}