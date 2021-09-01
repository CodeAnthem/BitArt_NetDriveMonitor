using System.Windows.Data;

namespace WPF.Main
{
	// https://thomaslevesque.com/2008/11/18/wpf-binding-to-application-settings-using-a-markup-extension/

	public class WindowSettingsBindingExtension : Binding
	{
		public WindowSettingsBindingExtension()
		{
			Initialize();
		}

		public WindowSettingsBindingExtension(string path)
			: base(path)
		{
			Initialize();
		}

		private void Initialize()
		{
			this.Source = windowSettings.Default;
			this.Mode = BindingMode.TwoWay;
		}
	}
}