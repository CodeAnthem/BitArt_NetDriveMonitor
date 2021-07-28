using BitArt_Netdrive_Helpers.models;

namespace NetDriveMonitor.models
{
	public class NetDriveMonitorModel : NetDriveBuddyModel
	{
		#region Public Properties

		public bool AutoReconnect { get; set; }

		public bool AutoDetect { get; set; }

		public bool SameLanOnly { get; set; }

		#endregion Public Properties

		#region Public Constructors

		public NetDriveMonitorModel(
									string letter, string hostName, string share,
			bool autoReconnect = false, bool sameLanOnly = false, bool autoDetect = false
			) : base(letter, hostName, share)
		{
			AutoReconnect = autoReconnect;
			SameLanOnly = sameLanOnly;
			AutoDetect = autoDetect;
		}

		#endregion Public Constructors
	}
}