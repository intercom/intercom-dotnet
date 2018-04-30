using System;

using Newtonsoft.Json;

namespace Intercom.Converters.ClassConverters
{
    public class DateTimeOffsetJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType.Equals(typeof(DateTimeOffset)) || objectType.Equals(typeof(DateTimeOffset?)));
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

            DateTimeOffset unixEpoch = _GetUnixEpoch();
            DateTimeOffset result = unixEpoch.AddSeconds(unixTimestamp);

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteRawValue("null");

                return;
            }

            DateTimeOffset unixEpoch = _GetUnixEpoch();
            DateTimeOffset dateTimeOffset = (DateTimeOffset)value;

            dateTimeOffset = dateTimeOffset.ToOffset(TimeSpan.Zero);

            long unixTimestamp = Convert.ToInt64((dateTimeOffset - unixEpoch).TotalSeconds);

            writer.WriteRawValue(unixTimestamp.ToString());
        }

        private DateTimeOffset _GetUnixEpoch()
        {
            return new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        }
    }
}
