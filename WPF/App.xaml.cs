using ControlzEx.Theming;
using MaterialDesignThemes.Wpf;
using Serilog;
using System.Windows;

namespace WPF
{
	/// <summary>
	/// Everything passed to <ref AppLoader>
	/// </summary>
	public partial class App : Application
	{
		private readonly AppLoader _loader = new AppLoader();
		public new static App Current => (App)Application.Current; // dunno for what I need this line

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			_loader.Init();
		}

		protected override void OnExit(ExitEventArgs e) => _loader.Exit(e);
	}
}