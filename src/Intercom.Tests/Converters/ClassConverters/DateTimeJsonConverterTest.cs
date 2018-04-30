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
        private readonly DateTimeJsonConverter _converter;

        public DateTimeJsonConverterTest()
        {
            _converter = new DateTimeJsonConverter();
        }

        [TestCase("608688900")]
        public void ReadJson_ForDateTime_ReturnsValidDateTime(string json)
        {
            using (StringReader stringReader = new StringReader(json))
            using (JsonReader jsonReader = new JsonTextReader(stringReader))
            {
                DateTime dateCreated = (DateTime)_converter.ReadJson(jsonReader, typeof(DateTime), null, null);

                Assert.AreEqual(new DateTime(1989, 4, 16, 0, 15, 0), dateCreated);
            }
        }

        [Test]
        public void WriteJson_ForDateTime_ReturnsValidUnixTimestamp()
        {
            throw new NotImplementedException();
        }
    }
}
