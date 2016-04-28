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
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Library.Converters.AttributeConverters
{
	public class MetadataJsonConverter : JsonConverter
	{
		public override bool CanConvert (Type objectType)
		{
			return objectType == typeof(User);
		}

		public override object ReadJson (JsonReader reader, 
		                                 Type objectType, 
		                                 object existingValue, 
		                                 JsonSerializer serializer)
		{
			JObject jobject = JObject.Load (reader);
			Metadata result = new Metadata ();

			foreach (var j in jobject) {
				if (j.Value is JObject) {
					
					JObject complex = j.Value as JObject;

					if (complex ["url"] != null && complex ["value"] != null) {
						
						result.AddMetadata (j.Key, new Metadata.RichLink () {
							url = complex ["url"].ToString (),
							value = complex ["value"].ToString ()
						});

					} else if (complex ["amount"] != null && complex ["currency"] != null) {

						int amount = 0;
						int.TryParse (complex ["amount"].ToString (), out amount);

						result.AddMetadata (j.Key, 
							new Metadata.MonetaryAmount () { 
								amount = amount, 
								currency = complex ["currency"].ToString ()
							});
					}
				} else {
					result.AddMetadata (j.Key, j.Value);
				}
			}

			return result;
		}

		public override void WriteJson (JsonWriter writer, 
		                                object value,
		                                JsonSerializer serializer)
		{
			Metadata metadata = value as Metadata;
			Dictionary<String, object> metadataDictionary = metadata.GetMetadata ();

			String s = JsonConvert.SerializeObject (metadataDictionary,
				           Formatting.None, 
				           new JsonSerializerSettings {
					NullValueHandling = NullValueHandling.Ignore
				});

			writer.WriteRawValue (s);
		}

		public override bool CanRead {
			get { return true; }
		}

	}
}