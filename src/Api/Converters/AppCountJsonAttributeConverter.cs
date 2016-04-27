using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Library
{
    public class AppCountJsonConverter: JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(AppCount);
        }

        public override object ReadJson(JsonReader reader, 
            Type objectType, 
            object existingValue,
            JsonSerializer serializer)
        {
            JObject j = JObject.Load(reader);
            int result = j.Value<int>("count");
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
    }
}