using NetDriveMonitor.interfaces;
using System;

namespace NetDriveMonitor.helpers
{
	public class NetDriveHelper
	{
		public bool Add(INetDrive drive)
		{
			drive.Letter = FixDriveLetter(drive.Letter); // make sure drive letter is only one char

			if (IsDriveAlreadyMapped(drive.Letter))
			{
				return false;
			}
			else
			{
				System.Diagnostics.Process.Start("net.exe", $@"use {drive.Letter}: \\{drive.HostName}\{drive.Share}").WaitForExit();
				return true;
			}
		}

		public bool Remove(INetDrive drive)
		{
			var fixedLetter = FixDriveLetter(drive.Letter); // make sure drive letter is only one char

			if (IsDriveAlreadyMapped(drive.Letter))
			{
				System.Diagnostics.Process.Start("net.exe", $"use /delete {fixedLetter}:").WaitForExit();
				return true;
			}
			else
			{
				return false;
			}
		}

		public void RemoveAll()
		{
			System.Diagnostics.Process.Start("net.exe", "use /delete *").WaitForExit();
		}

		public bool IsDriveAlreadyMapped(string letter)
		{
			if (letter.Length > 1)
			{
				letter = letter.Substring(0, 1);
			}

			string[] driveList = Environment.GetLogicalDrives(); // get all logical drives

			for (int i = 0; i < driveList.Length; i++)
			{
				if (letter + ":\\" == driveList[i])
				{
					return true;
				}
			}
			return false;
		}

		private string FixDriveLetter(string letter)
		{
			if (letter.Length > 1)
			{
				letter = letter.Substring(0, 1);
			}

			return letter;
		}
	}
}