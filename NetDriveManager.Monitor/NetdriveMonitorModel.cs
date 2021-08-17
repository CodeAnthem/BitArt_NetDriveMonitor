﻿using BitArt_Netdrive_Helpers.models;

namespace NetDriveManager.Monitor
{
	public class NetdriveMonitorModel : NetDriveBuddyModel
	{
		#region Public Properties

		public bool AutoConnectIfAvailable { get; set; }
		public bool AutoConnectIfAccessed { get; set; }
		public bool AutoConnectLanOnly { get; set; }

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