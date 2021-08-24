using WPF.Main;

namespace WPF.AppUI.Frame
{
	public class HeaderViewModel : ViewModelBase
	{
		public string HeaderTitle { get => _appInfo.HeaderTitle; }
		public string HeaderSubTitle { get => _appInfo.HeaderSubTitle; }

		private readonly AppInfo _appInfo;

		public HeaderViewModel(AppInfo appInfo)
		{
			_appInfo = appInfo;
		}
	}
}