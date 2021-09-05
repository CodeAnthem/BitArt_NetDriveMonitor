using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveManager.Monitor.Models
{
	public class NetDriveMonitorSettings : ObservableObject, INetDriveMonitorSettings
	{
		#region Private Fields

		private bool isAutoConnectIfAvailable;

		#endregion

		#region Public Properties

		public bool IsAutoConnectIfAvailable
		{
			get { return isAutoConnectIfAvailable; }
			set { SetProperty(ref isAutoConnectIfAvailable, value); }
		}

		#endregion
	}
}