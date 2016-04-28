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

namespace Library.Converters.ClassConverters
{
    public class UserCountJsonConverter: JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, 
                                        Type objectType, 
                                        object existingValue,
                                        JsonSerializer serializer)
        {
            JObject j = JObject.Load(reader);
            JArray result = null;

            if (objectType == typeof(UserTagCount))
                result = j["user"]["tag"] as JArray;
            else
                result = j["user"]["segment"] as JArray;

            Dictionary<String, int> count = new Dictionary<String, int>();

            foreach (var r in result)
            {
                JProperty c = r.First as JProperty;

                if (c != null)
                {
                    int value = 0;
                    int.TryParse(c.Value.ToString(), out value);
                    count.Add(c.Name, value);
                }
            }

            if (objectType == typeof(UserTagCount))
                return  new UserTagCount() { tags = count };
            else
                return  new UserSegmentCount() { segments = count };
        }

        public override void WriteJson(JsonWriter writer, 
                                       object value,
                                       JsonSerializer serializer)
        {
            String s = JsonConvert.SerializeObject(value,
                           Formatting.None,
                           new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            writer.WriteRawValue(s);
        }

        public override bool CanRead
        {
            get { return true; }
        }
    }
}