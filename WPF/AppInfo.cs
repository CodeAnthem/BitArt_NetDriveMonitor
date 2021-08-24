namespace WPF
{
	public class AppInfo
	{
		private const string _appName = "Netdrive Manager";
		private const string _author = "BitArt";

		public string AppName { get; } = $"{_appName}";
		public string AppVersion { get; } = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
		public string AppAuthor { get; } = $"{_author}";
		public string WindowTitle { get; } = $"{_appName}";
		public string HeaderTitle { get; } = $"{_appName}";
		public string HeaderSubTitle { get; } = "App under construction";
		public string FooterTitle { get; } = $"{_author} - {_appName}";
	}
}