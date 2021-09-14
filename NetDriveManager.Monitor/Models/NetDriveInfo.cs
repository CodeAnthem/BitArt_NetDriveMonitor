using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveManager.Monitor.Models
{
	public class NetDriveInfo
	{
		#region Public Properties

		public string HostName { get; set; }
		public char Letter { get; set; }
		public string Share { get; set; }

		#endregion

		#region Public Constructors

		public NetDriveInfo(char letter, string hostName, string share)
		{
			Letter = letter;
			HostName = hostName;
			Share = share;
		}

		public NetDriveInfo(string letter, string hostName, string share)
		{
			Letter = char.ToUpper(letter[0]);
			HostName = hostName;
			Share = share;
		}

		#endregion
	}
}