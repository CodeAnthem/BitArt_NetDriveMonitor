using Microsoft.VisualBasic;
using NetDriveMonitor.interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NetDriveMonitor.components
{
	public class SimpleNetDriveMapper
	{
		private Dictionary<string, List<INetDrive>> _driveDict;

		public SimpleNetDriveMapper()
		{
			_driveDict = new Dictionary<string, List<INetDrive>>();
		}

		public void CheckDrive(string hostName, bool state)
		{
		}

		internal bool AddDrive(INetDrive drive)
		{
			//_driveDict.Clear(); // clear list
			try
			{
				if (!_driveDict.ContainsKey(drive.HostName))
				{
					_driveDict.Add(drive.HostName, new List<INetDrive>());
				}

				_driveDict[drive.HostName].Add(drive);
				return true;
			}
			catch (Exception e)
			{
				string error = "Something went wrong. Failed to add a drive to the mapper.\n\n" +
								" - [ Drive Infos ]\n" +
								$" - Drive: '{drive.Letter}'\n" +
								$" - Server: '{drive.HostName}'\n" +
								$" - Share: '{drive.Share}'";

				throw new NullReferenceException(error, e);
				Debug.WriteLine(error);
				return false;
			}
		}

		internal void Clear()
		{
			_driveDict.Clear();
		}
	}
}