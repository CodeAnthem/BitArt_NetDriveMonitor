using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace NetDriveManager.Monitor.Models
{
	public class NetDriveOptions : ObservableObject
	{
		#region Private Fields

		private bool _autoConnectIfAccessed;
		private bool _autoConnectIfAvailable;
		private bool _autoConnectLanOnly;

		#endregion

		#region Public Properties

		public bool AutoConnectIfAccessed
		{
			get { return _autoConnectIfAccessed; }
			set { SetProperty(ref _autoConnectIfAccessed, value); }
		}

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

		public NetDriveOptions(bool autoConnectIfAccessed, bool autoConnectIfAvailable, bool autoConnectLanOnly)
		{
			AutoConnectIfAccessed = autoConnectIfAccessed;
			AutoConnectIfAvailable = autoConnectIfAvailable;
			AutoConnectLanOnly = autoConnectLanOnly;
		}

		public NetDriveOptions()
		{
			AutoConnectIfAccessed = false;
			AutoConnectIfAvailable = false;
			AutoConnectLanOnly = false;
		}

		#endregion
	}
}