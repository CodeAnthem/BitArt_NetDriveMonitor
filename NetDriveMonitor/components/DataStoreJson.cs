using NetDriveMonitor.interfaces;
using NetDriveMonitor.models;
using Newtonsoft.Json;
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

		public List<NetDriveMonitorModel> Get()
		{
			if (_saveFile.Exists && _saveFile.Length > 0)
			{
				// json import
				string jsonString = File.ReadAllText(_saveFile.FullName);
				var netDriveModels = JsonConvert.DeserializeObject<List<NetDriveMonitorModel>>(jsonString);
				return netDriveModels.ToList<NetDriveMonitorModel>();
			}

			if (_useDummyIfEmpty)
			{
				return GetDummyDrives(); // testing stuff
			}
			return null;
		}

		public bool Save(List<NetDriveMonitorModel> netDrives)
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

		private List<NetDriveMonitorModel> GetDummyDrives()
		{
			return new List<NetDriveMonitorModel>
			{
				new NetDriveMonitorModel("H:", "dp-nas10", "home", true),
				new NetDriveMonitorModel("V:", "dp-nas10", "video")
			};
		}

		#endregion Private Methods
	}
}