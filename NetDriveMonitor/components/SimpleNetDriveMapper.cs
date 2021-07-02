//using NetDriveMonitor.interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace NetDriveMonitor.components
//{
//	public class SimpleNetDriveMapper
//	{
//		private readonly Dictionary<string, List<INetDrive>> _driveDict;
//		private NetDriveHelper _ndHelper;

//		public SimpleNetDriveMapper()
//		{
//			_driveDict = new Dictionary<string, List<INetDrive>>();
//			_ndHelper = new NetDriveHelper();
//		}

//		public void CheckDrive(string hostName, bool state)
//		{
//			// get list of affected drives
//			var changedDrivesList = _driveDict[hostName].ToList();

//			if (changedDrivesList?.Count > 0)
//			{
//				foreach (INetDrive drive in changedDrivesList)
//				{
//					if (state)
//					{
//						// connect drive
//						bool wasSuccess = _ndHelper.Add(drive);
//						if (wasSuccess)
//						{
//							Console.WriteLine(@$"Drive connected: {drive.Letter} \\{drive.HostName}\{drive.Share}");
//						}
//					}
//					else
//					{
//						// disconnect drive
//						bool wasSuccess = _ndHelper.Remove(drive);
//						if (wasSuccess)
//						{
//							Console.WriteLine(@$"Drive disconnected: {drive.Letter} \\{drive.HostName}\{drive.Share}");
//						}
//					}
//				}
//			}

//			Console.WriteLine("Checking {0} drives", changedDrivesList.Count);
//		}

//		internal bool AddDrive(INetDrive drive)
//		{
//			//_driveDict.Clear(); // clear list
//			try
//			{
//				if (!_driveDict.ContainsKey(drive.HostName))
//				{
//					_driveDict.Add(drive.HostName, new List<INetDrive>());
//				}

//				_driveDict[drive.HostName].Add(drive);
//				return true;
//			}
//			catch (Exception e)
//			{
//				string error = "Something went wrong. Failed to add a drive to the mapper.\n\n" +
//								" - [ Drive Infos ]\n" +
//								$" - Drive: '{drive.Letter}'\n" +
//								$" - Server: '{drive.HostName}'\n" +
//								$" - Share: '{drive.Share}'";

//				throw new NullReferenceException(error, e);
//			}
//		}

//		internal void Clear()
//		{
//			_driveDict.Clear();
//		}
//	}
//}