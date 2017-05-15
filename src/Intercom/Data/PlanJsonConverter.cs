using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Intercom.Data
{
    public class PlanJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Plan plan = value as Plan;

            writer.WriteValue(plan.name);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> values = serializer.Deserialize<Dictionary<string, object>>(reader);

            Plan plan = new Plan();

            plan.id = values["id"] as string;
            plan.type = values["type"] as string;
            plan.name = values["name"] as string;

            return plan;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Plan);
        }
    }
}
