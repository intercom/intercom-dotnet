using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Intercom.Clients;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Intercom.Converters.ClassConverters
{
    public class ConversationAdminCountJsonConverter: JsonConverter
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
                JArray result = j["conversation"]["admin"] as JArray;
                List<ConversationAdminCount.AdminCount> admins = new List<ConversationAdminCount.AdminCount>();

                foreach (var r in result)
                {
                    int open = 0;
                    int closed = 0;
                    int.TryParse(r["open"].Value<String>(), out open);
                    int.TryParse(r["closed"].Value<String>(), out closed);

                    admins.Add(new ConversationAdminCount.AdminCount()
                        { 
                            id = r["id"].Value<String>(),
                            name = r["name"].Value<String>(),
                            open = open,
                            closed = closed
                        });
                }

                return new ConversationAdminCount() { admins = admins };
            }
            catch (Exception ex)
            {
                throw new JsonConverterException("Error while serializing ConversationAdminCount endpoint json result.", ex)
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