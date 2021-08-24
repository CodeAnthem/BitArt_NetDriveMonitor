using WPF.Main;

namespace WPF.AppUI.Frame
{
	public class FooterViewModel : ViewModelBase
	{
		public string FooterText { get => _appInfo.FooterTitle; }

		private readonly AppInfo _appInfo;

		public FooterViewModel(AppInfo appInfo)
		{
			_appInfo = appInfo;
		}
	}
}