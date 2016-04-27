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
    public class ConversationAdminCountJsonClassConverter: JsonConverter
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
            JArray result = j["conversation"]["admin"] as JArray;
            List<ConversationAdminCount.AdminCount> admins = new List<ConversationAdminCount.AdminCount>();

            foreach (var r in result)
            {
                admins.Add(new ConversationAdminCount.AdminCount()
                    { 
                        id = r["id"].Value<String>(),
                        name = r["name"].Value<String>(),
                        open = r["open"].Value<String>(),
                        closed = r["closed"].Value<String>()
                    });
            }

            return new ConversationAdminCount() { admins = admins };
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