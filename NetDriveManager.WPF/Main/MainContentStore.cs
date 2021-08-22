using System;
using System.Windows.Controls;

namespace NetDriveManager.WPF.Main
{
	public class MainContentStore
	{
		public event Action OnContentChanged;

		private ContentControl _control;
		public ContentControl Control
		{
			get => _control;
			set
			{
				_control = value;
				OnContentChanged?.Invoke();
			}
		}
	}
}