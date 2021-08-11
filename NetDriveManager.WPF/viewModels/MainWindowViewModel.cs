using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.utilities.contentController.services;
using System.Windows.Controls;

namespace NetDriveManager.WPF.viewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		private readonly IContentControllerService _cc;
		public ContentControl CCHeader { get; }
		public ContentControl CCMain { get; }
		public ContentControl CCNav { get; }
		public ContentControl CCFooter { get; }

		public MainWindowViewModel(IContentControllerService contentControllerService)
		{
			_cc = contentControllerService;
			CCHeader = _cc.GetUserControl(nameof(HeaderControl));
			MyStringProperty = "test property of main view model";
		}

		public string MyStringProperty { get; set;  }
	}
}