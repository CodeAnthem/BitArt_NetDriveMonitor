using NetDriveMonitor.interfaces;
using NetDriveMonitor.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetDriveMonitor.components
{
	public class DataStoreJson : IDatastore
	{
		#region Public Fields

		public FileInfo _saveFile;

		private bool _useDummyIfEmpty;

		#endregion Public Fields

		#region Public Constructors

		public DataStoreJson(string saveFilePath, bool useDummyIfEmpty = false)
		{
			_saveFile = new FileInfo(saveFilePath);
			_useDummyIfEmpty = useDummyIfEmpty;
		}

		#endregion Public Constructors

		#region Public Methods

		public List<INetDrive> Get()
		{
			if (_saveFile.Exists && _saveFile.Length > 0)
			{
				// json import
				string jsonString = File.ReadAllText(_saveFile.FullName);

				List<NetDriveModel> netDriveModels;
				netDriveModels = JsonConvert.DeserializeObject<List<NetDriveModel>>(jsonString);

				return netDriveModels.ToList<INetDrive>();
			}
			else
			{
				if (_useDummyIfEmpty)
				{
					return GetDummyDrives(); // testing stuff
				}
				return null;
			}
		}

		public bool Save(List<INetDrive> netDrives)
		{
			if (netDrives?.Count > 0)
			{
				if (!Directory.Exists(_saveFile.DirectoryName))
				{
					Directory.CreateDirectory(_saveFile.DirectoryName);
				}

				string jsonString = JsonConvert.SerializeObject(netDrives, Formatting.Indented);
				File.WriteAllText(_saveFile.FullName, jsonString);
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion Public Methods

		#region Private Methods

		private List<INetDrive> GetDummyDrives()
		{
			return new List<INetDrive>
			{
				new NetDriveModel("H:", "home", "dp-nas10"),
				new NetDriveModel("V:", "video", "dp-nas10")
			};
		}

		#endregion Private Methods
	}
}