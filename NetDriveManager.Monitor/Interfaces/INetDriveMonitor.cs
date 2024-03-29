﻿using System;
using System.Collections.ObjectModel;

namespace NetDriveManager.Monitor.Interfaces
{
	public interface INetDriveMonitor
	{
		event Action EnabledStatusChanged;

		IDataAccess DataAccess { get; }
		INetDriveHelper DriveHelper { get; }
		ObservableCollection<INetDrive> Drives { get; }
		bool IsEnabled { get; }
		INetDriveMonitorSettings Settings { get; }

		bool Activate();

		bool Deactivate();
	}
}