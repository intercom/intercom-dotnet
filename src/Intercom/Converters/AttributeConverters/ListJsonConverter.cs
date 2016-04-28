using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Library.Converters.AttributeConverters
{
	public class ListJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(User);
		}

		public override object ReadJson(JsonReader reader, 
			Type objectType, 
			object existingValue, 
			JsonSerializer serializer)
		{
			JObject jobject = JObject.Load(reader);
			Object result = null;

			if (objectType == typeof(List<Company>))
				result = GetList<Company>(jobject, "companies");
			else if (objectType == typeof(List<SocialProfile>))
				result = GetList<SocialProfile>(jobject, "social_profiles");
			else if (objectType == typeof(List<Tag>))
				result = GetList<Tag>(jobject, "tags");
			else if (objectType == typeof(List<Segment>))
				result = GetList<Segment>(jobject, "segments");

			return result;
		}

		public override void WriteJson(JsonWriter writer, 
			object value,
			JsonSerializer serializer)
		{
			String s = JsonConvert.SerializeObject (value,
				Formatting.None, 
				new JsonSerializerSettings {
					NullValueHandling = NullValueHandling.Ignore
				});
		
			writer.WriteRawValue (s);
		}

		public override bool CanRead {
			get { return true;}
		}

		private List<T> GetList<T>(JObject jobject, String key)
			where T: class
		{
			var value = jobject.GetValue (key);
			var result = (JsonConvert.DeserializeObject (value.ToString (), typeof(T[])) as T[]).ToList ();
			return result;
		}
	}
}