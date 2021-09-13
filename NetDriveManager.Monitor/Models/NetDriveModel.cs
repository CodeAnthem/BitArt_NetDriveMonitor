using Microsoft.Toolkit.Mvvm.ComponentModel;
using NetDriveManager.Monitor.Interfaces;
using Newtonsoft.Json;

namespace NetDriveManager.Monitor.Models
{
	public class NetDriveModel : ObservableObject, INetDrive
	{
		#region Public Constructors

		public NetDriveModel(string letter, string hostName, string share
			, bool autoConnectIfAvailable, bool autoConnectLanOnly)
		{
			Info = new NetDriveInfo(letter, hostName, share);
			Options = new NetDriveOptions(autoConnectIfAvailable, autoConnectLanOnly);
			Status = new NetDriveStatus();
		}

		public NetDriveModel()
		{
			Info = new NetDriveInfo("", "", "");
			Options = new NetDriveOptions();
			Status = new NetDriveStatus();
		}

		public NetDriveModel(NetDriveInfo info, NetDriveOptions options, NetDriveStatus status)
		{
			Info = info;
			Options = options;
			Status = status;
		}

		#endregion

		#region Private Methods

		private static bool IsEqual(NetDriveModel left, NetDriveModel right)
		{
			if (left is null || right is null)
			{
				return false;
			}
			return left.Info == right.Info;
		}

		#endregion

		#region Public Properties

		public NetDriveInfo Info { get; set; }

		public NetDriveOptions Options { get; set; }

		[JsonIgnore]
		public NetDriveStatus Status { get; set; }

		#endregion

		#region Public Methods

		public static bool operator !=(NetDriveModel left, NetDriveModel right) => !IsEqual(left, right);

		public static bool operator ==(NetDriveModel left, NetDriveModel right) => IsEqual(left, right);

		public override bool Equals(object o)
		{
			return IsEqual(this, o as NetDriveModel);
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public override string ToString()
		{
			return $@"{Info.Letter}: \\{Info.HostName}\{Info.Share}";
		}

		#endregion
	}
}