using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace NetDriveManager.Monitor.Models
{
	public class NetDriveOptions : ObservableObject
	{
		#region Private Fields

		private bool _autoConnectIfAvailable;
		private bool _autoConnectLanOnly;

		#endregion

		#region Public Properties

		public bool AutoConnectIfAvailable
		{
			get { return _autoConnectIfAvailable; }
			set { SetProperty(ref _autoConnectIfAvailable, value); }
		}

		public bool AutoConnectLanOnly
		{
			get { return _autoConnectLanOnly; }
			set { SetProperty(ref _autoConnectLanOnly, value); }
		}

		#endregion

		#region Public Constructors

		public NetDriveOptions(bool autoConnectIfAvailable, bool autoConnectLanOnly)
		{
			AutoConnectIfAvailable = autoConnectIfAvailable;
			AutoConnectLanOnly = autoConnectLanOnly;
		}

		public NetDriveOptions()
		{
			AutoConnectIfAvailable = false;
			AutoConnectLanOnly = false;
		}

		#endregion
	}
}