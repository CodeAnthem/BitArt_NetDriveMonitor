﻿using System.Collections.Generic;
using System.IO;

namespace NetDriveManager.Data.JSON
{
	internal class DataAccessorJsonFile : IDataAccessor
	{
		#region Private Fields

		private const string _filePath = @"Save\drives.json";
		private static readonly string _applicationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
		private readonly FileInfo _jsonFile;
		private readonly string _jsonFilePath;

		#endregion

		#region Public Constructors

		public DataAccessorJsonFile()
		{
			_jsonFilePath = Path.Combine(_applicationPath, _filePath);
			_jsonFile = new FileInfo(_jsonFilePath);
		}

		#endregion

		#region Public Methods

		public List<NetdriveMonitorModel> GetDrives0RNull()
		{
			if (_jsonFile.Exists)
			{
				string jsonString = File.ReadAllText(_jsonFile.FullName);
				var list = JsonConvert.DeserializeObject<List<NetdriveMonitorModel>>(jsonString);
				Log.Debug("Loaded {amount} drives of JSON file", list.Count);
				return list;
			}
			Log.Warning("No JSON file found, returning null");
			return null;
		}

		public bool SaveDrives(List<NetdriveMonitorModel> netdrivesList)
		{
			var anyDrives = netdrivesList.Count > 0;
			if (anyDrives)
			{
				if (!Directory.Exists(_jsonFile.DirectoryName))
					Directory.CreateDirectory(_jsonFile.DirectoryName);

				string jsonString = JsonConvert.SerializeObject(netdrivesList, Formatting.Indented);
				File.WriteAllText(_jsonFile.FullName, jsonString);
				Log.Information("Successfully saved {amount} drives to JSON file", netdrivesList.Count);
				return true;
			}
			Log.Warning("Saving failed, no data?");
			return false;
		}

		#endregion
	}
}