using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intercom.Clients;
using Intercom.Converters.ClassConverters;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;

namespace Intercom.Test
{
    [TestFixture()]
    public class AppCountJsonConverterTest : TestBase
    {
        private AppCountJsonConverter appCountJsonConverter;

        public AppCountJsonConverterTest()
        {
            this.appCountJsonConverter = new AppCountJsonConverter();
        }

        [Test()]
        public void ReadJson_ForConversationAppCount_ReturnsValidCount()
        {
            String input = "{\"type\":\"count.hash\",\"company\":{\"count\":6},\"user\":{\"count\":2025082},\"lead\":{\"count\":1},\"tag\":{\"count\":17},\"segment\":{\"count\":7}}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);
            AppCount appCount = appCountJsonConverter.ReadJson(reader, typeof(int), null, null) as AppCount;

            Assert.AreEqual(6, appCount.company);
            Assert.AreEqual(2025082, appCount.user);
        }

        [Test()]
        public void ReadJson_InvalidJson_ThrowsException()
        {
            String input = "{\"type\t\":2025082},\"lead\":{\"count\":{\"count\":17},\"segment\":{\"count\":7}}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);

            Assert.Throws<JsonConverterException>(() => appCountJsonConverter.ReadJson(reader, typeof(int), null, null));
        }
    }
}