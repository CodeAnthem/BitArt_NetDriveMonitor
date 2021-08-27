using System.Windows.Data;

namespace WPF.Main
{
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