using System;

using Newtonsoft.Json;

namespace Intercom.Converters.ClassConverters
{
    public class DateTimeJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType.Equals(typeof(DateTime)) || objectType.Equals(typeof(DateTime?)));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            reader.Read();

            object value = reader.Value;

            if (value == null)
            {
                return null;
            }

            long unixTimestamp;

            try
            {
                unixTimestamp = Convert.ToInt64(value);
            }

            catch (InvalidCastException ex)
            {
                throw new FormatException("Dates must be represented as UNIX timestamps in JSON.", ex);
            }

            DateTime unixEpoch = new DateTime(1970, 1, 1);
            DateTime result = unixEpoch.AddSeconds(unixTimestamp);

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteRawValue("null");

                return;
            }

            DateTime unixEpoch = new DateTime(1970, 1, 1);
            DateTime dateTime = (DateTime)value;

            long unixTimestamp = Convert.ToInt64((dateTime - unixEpoch).TotalSeconds);

            writer.WriteRawValue(unixTimestamp.ToString());
        }
    }
}
