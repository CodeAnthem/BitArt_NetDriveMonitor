using NetDriveMonitor.interfaces;
using NetDriveMonitor.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveMonitor.helpers
{
	public class NetDriveHelper
	{
		public bool Add(NetDriveModel drive)
		{
			drive.Letter = FixDriveLetter(drive.Letter); // make sure drive letter is only one char

			if (IsDriveAlreadyMapped(drive.Letter))
			{
				return false;
			}
			else
			{
				System.Diagnostics.Process.Start("net.exe", $@"use {drive.Letter}: \\{drive.Server}\{drive.Share}").WaitForExit();
				return true;
			}

			//System.Diagnostics.Process.Start("net.exe", @"use K: \\my.server.com\Websites\Mail / user:MyDomain\admin MySuperSecretPassword").WaitForExit();
			//Utility.NetworkDrive.MapNetworkDrive("R", @"\\unc\path");
			//var dirs = Directory.GetDirectories("R:"); // got many nice directories...
			//Utility.NetworkDrive.DisconnectNetworkDrive("R", true);
		}

		public void Remove()
		{
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