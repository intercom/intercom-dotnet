using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Library.Clients;
using Library.Core;
using Library.Data;
using Library.Exceptions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using Library.Converters.AttributeConverters;

namespace Library.Test
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
            String input = "{\"count\":6}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);
            int company = (int)appCountJsonConverter.ReadJson(reader, typeof(int), null, null);

            Assert.AreEqual(6, company);
        }

        [Test()]
        [ExpectedException(typeof(JsonConverterException))]
        public void ReadJson_InvalidJson_ThrowsException()
        {
            String input = "\"count\":6}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);

            appCountJsonConverter.ReadJson(reader, typeof(int), null, null);
        }
    }
}