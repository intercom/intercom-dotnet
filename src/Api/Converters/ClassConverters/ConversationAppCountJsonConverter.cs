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
    public class ConversationAppCountJsonConverter: JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ConversationAdminCount);
        }

        public override object ReadJson(JsonReader reader, 
            Type objectType, 
            object existingValue,
            JsonSerializer serializer)
        {
            JObject j = JObject.Load(reader);
            JObject result = j["conversation"] as JObject;
            ConversationAppCount count = new ConversationAppCount();
            count.assigned = result["assigned"].Value<int>();
            count.unassigned = result["unassigned"].Value<int>();
            count.open = result["open"].Value<int>();
            count.closed = result["closed"].Value<int>();
            return count;
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