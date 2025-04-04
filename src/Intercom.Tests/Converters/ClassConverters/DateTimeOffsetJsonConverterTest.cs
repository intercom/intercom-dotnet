using System;
using System.IO;

using Intercom.Test;
using Newtonsoft.Json;
using NUnit.Framework;

using Intercom.Converters.ClassConverters;

namespace Intercom.Tests.Converters.ClassConverters
{
    [TestFixture]
    public class DateTimeOffsetJsonConverterTest : TestBase
    {
        private const string _DateCreatedISO = "1989-04-16T00:15:00Z";
        private const string _DateCreatedUnix = "608688900";
        private const string _Null = "null";

        private readonly DateTimeOffsetJsonConverter _converter;

        public DateTimeOffsetJsonConverterTest()
        {
            _converter = new DateTimeOffsetJsonConverter();
        }

        [TestCase(_DateCreatedUnix)]
        public void ReadJson_ForDateTimeOffset_ReturnsValidDateTimeOffset(string json)
        {
            using (StringReader stringReader = new StringReader(json))
            using (JsonReader jsonReader = new JsonTextReader(stringReader))
            {
                DateTimeOffset dateCreated = (DateTimeOffset)_converter.ReadJson(jsonReader, typeof(DateTimeOffset), null, null);

                Assert.AreEqual(_ParseUtcDateString(_DateCreatedISO), dateCreated);
            }
        }

        [TestCase(_DateCreatedISO)]
        public void WriteJsonForDateTimeOffset_ForcesUtc(string input)
        {
            DateTimeOffset inputDate;

            try
            {
                inputDate = _ParseUtcDateString(input);
            }

            catch (Exception ex)
            {
                throw new Exception("Failed to properly parse the test input.", ex);
            }

            inputDate.ToOffset(TimeSpan.FromHours(-5));

            using (StringWriter stringWriter = new StringWriter())
            using (JsonWriter jsonWriter = new JsonTextWriter(stringWriter))
            {
                _converter.WriteJson(jsonWriter, inputDate, null);

                Assert.AreEqual(_DateCreatedUnix, stringWriter.GetStringBuilder().ToString());
            }
        }

        [TestCase(_Null)]
        public void ReadJson_ForNullableDateTimeOffset_ReturnsNull(string json)
        {
            using (StringReader stringReader = new StringReader(json))
            using (JsonReader jsonReader = new JsonTextReader(stringReader))
            {
                DateTimeOffset? dateCreated = (DateTimeOffset?)_converter.ReadJson(jsonReader, typeof(DateTimeOffset?), null, null);

                Assert.AreEqual(null, dateCreated);
            }
        }

        [TestCase(null)]
        public void WriteJsonForNullableDateTimeOffset_ReturnsNull(DateTimeOffset? input)
        {
            using (StringWriter stringWriter = new StringWriter())
            using (JsonWriter jsonWriter = new JsonTextWriter(stringWriter))
            {
                _converter.WriteJson(jsonWriter, input, null);

                Assert.AreEqual(_Null, stringWriter.GetStringBuilder().ToString());
            }
        }

        [TestCase(_DateCreatedISO)]
        public void WriteJson_ForDateTimeOffset_ReturnsValidUnixTimestamp(string input)
        {
            DateTimeOffset inputDate;

            try
            {
                inputDate = _ParseUtcDateString(input);
            }

            catch (Exception ex)
            {
                throw new Exception("Failed to properly parse the test input.", ex);
            }

            using (StringWriter stringWriter = new StringWriter())
            using (JsonWriter jsonWriter = new JsonTextWriter(stringWriter))
            {
                _converter.WriteJson(jsonWriter, inputDate, null);

                Assert.AreEqual(_DateCreatedUnix, stringWriter.GetStringBuilder().ToString());
            }
        }

        private DateTimeOffset _ParseUtcDateString(string input)
        {
            return DateTimeOffset.Parse(input);
        }
    }
}