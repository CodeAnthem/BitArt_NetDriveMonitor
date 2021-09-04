using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace NetDriveManager.Monitor.Models
{
	public class NetDriveStatus : ObservableObject
	{
		#region Private Fields

		private bool _isConnected;
		private bool _isHostAvailable;

		#endregion

		#region Public Properties

		public bool IsConnected
		{
			get { return _isConnected; }
			set { SetProperty(ref _isConnected, value); }
		}

		public bool IsHostAvailable
		{
			get { return _isHostAvailable; }
			set { SetProperty(ref _isHostAvailable, value); }
		}

		#endregion
	}
}