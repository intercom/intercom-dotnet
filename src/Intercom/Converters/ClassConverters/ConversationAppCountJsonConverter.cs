using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Intercom.Converters.ClassConverters
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
            JObject j = null;

            try
            {
                j = JObject.Load(reader);
                JObject result = j["conversation"] as JObject;
                ConversationAppCount count = new ConversationAppCount();
                count.assigned = result["assigned"].Value<int>();
                count.unassigned = result["unassigned"].Value<int>();
                count.open = result["open"].Value<int>();
                count.closed = result["closed"].Value<int>();
                return count;
            }
            catch (Exception ex)
            {
                throw new JsonConverterException("Error while serializing ConversationAppCount endpoint json result.", ex)
                {
                    Json = j == null ? String.Empty : j.ToString(),
                    SerializationType = objectType.FullName
                };
            }
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