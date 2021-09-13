using NetDriveManager.Monitor.Interfaces;
using NetDriveManager.Monitor.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace NetDriveManager.Monitor.components.dataAccess
{
	public class NetDriveConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(INetDrive);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jo = JObject.Load(reader);

			string letter = (string)jo["Info"]["Letter"];
			string hostName = (string)jo["Info"]["HostName"];
			string share = (string)jo["Info"]["Share"];

			bool autoConnectIfAvailable = (bool)jo["Options"]["AutoConnectIfAvailable"];
			bool autoConnectLanOnly = (bool)jo["Options"]["AutoConnectLanOnly"];

			var info = new NetDriveInfo(letter, hostName, share);
			var options = new NetDriveOptions(autoConnectIfAvailable, autoConnectLanOnly);
			var status = new NetDriveStatus();

			return new NetDriveModel(info, options, status);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}