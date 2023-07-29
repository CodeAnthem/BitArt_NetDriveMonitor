using Stylet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NetDriveManager.WPF
{
	public class RootViewModel : Screen
	{
		private readonly IWindowManager _windowManager;

		public RootViewModel(IWindowManager windowManager)
		{
			_windowManager = windowManager;
			MessageBoxViewModel.ButtonLabels[MessageBoxResult.No] = "No, thanks";

			this._windowManager.ShowMessageBox("Do you want breakfast?",
											   buttons: MessageBoxButton.YesNo,
											   buttonLabels: new Dictionary<MessageBoxResult, string>()
					{
			{ MessageBoxResult.Yes, "Yes please!" },
					});

			// Will display a MessageBox with the buttons "Yes please!" and "No, thanks"
			//_windowManager.ShowMessageBox("Hehehehehe");
		}
	}
}