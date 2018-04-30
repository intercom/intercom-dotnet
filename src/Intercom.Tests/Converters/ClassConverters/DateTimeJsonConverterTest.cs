using System;
using System.IO;

using Intercom.Test;
using Newtonsoft.Json;
using NUnit.Framework;

using Intercom.Converters.ClassConverters;

namespace Intercom.Tests.Converters.ClassConverters
{
    [TestFixture]
    public class DateTimeJsonConverterTest : TestBase
    {
        private const string _DateCreatedISO = "1989-04-16T00:15:00Z";
        private const string _DateCreatedUnix = "608688900";

        private readonly DateTimeJsonConverter _converter;

        public DateTimeJsonConverterTest()
        {
            _converter = new DateTimeJsonConverter();
        }

        [TestCase(_DateCreatedUnix)]
        public void ReadJson_ForDateTime_ReturnsValidDateTime(string json)
        {
            using (StringReader stringReader = new StringReader(json))
            using (JsonReader jsonReader = new JsonTextReader(stringReader))
            {
                DateTime dateCreated = (DateTime)_converter.ReadJson(jsonReader, typeof(DateTime), null, null);

                Assert.AreEqual(_ParseUtcDateString(_DateCreatedISO), dateCreated);
            }
        }

        [TestCase(_DateCreatedISO)]
        public void WriteJson_ForDateTime_ReturnsValidUnixTimestamp(string input)
        {
            DateTime inputDate;

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

        private DateTime _ParseUtcDateString(string input)
        {
            return DateTimeOffset.Parse(input).UtcDateTime;
        }
    }
}
