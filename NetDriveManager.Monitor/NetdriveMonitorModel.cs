using BitArt_Netdrive_Helpers.models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace NetDriveManager.Monitor
{
	public class NetdriveMonitorModel : NetDriveBuddyModel
	{
		#region Private Fields

		private bool _isConnected;
		private bool _isHostAvailable;

		#endregion

		#region Public Properties

		public bool AutoConnectIfAccessed { get; set; }
		public bool AutoConnectIfAvailable { get; set; }
		public bool AutoConnectLanOnly { get; set; }

		[JsonIgnore]
		public bool IsConnected
		{
			get { return _isConnected; }
			set { SetProperty(ref _isConnected, value); }
		}

		[JsonIgnore]
		public bool IsHostAvailable
		{
			get { return _isHostAvailable; }
			set { SetProperty(ref _isHostAvailable, value); }
		}

		#endregion Public Properties

		#region Public Constructors

		public NetdriveMonitorModel(
			string letter, string hostName, string share,
			bool autoConnectIfAvailable = false, bool autoConnectIfAccessed = false, bool autoConnectLanOnly = false
			) : base(letter, hostName, share)
		{
			AutoConnectIfAvailable = autoConnectIfAvailable;
			AutoConnectIfAccessed = autoConnectIfAccessed;
			AutoConnectLanOnly = autoConnectLanOnly;
		}

		#endregion Public Constructors
	}
}