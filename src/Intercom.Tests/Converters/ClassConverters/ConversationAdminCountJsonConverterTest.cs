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
    public class ConversationAdminCountJsonConverterTest : TestBase
    {
        private ConversationAdminCountJsonConverter conversationAdminCountJsonConverter;

        public ConversationAdminCountJsonConverterTest()
        {
            this.conversationAdminCountJsonConverter = new ConversationAdminCountJsonConverter();
        }

        [Test()]
        public void ReadJson_ForConversationAppCount_ReturnsValidCount()
        {
            String input = "{\"type\":\"count\",\"conversation\":{\"admin\":[{\"name\":\"AAA\",\"id\":\"29\",\"open\":3,\"closed\":11},{\"name\":\"BBB\",\"id\":\"10\",\"open\":2,\"closed\":0},{\"name\":\"CCC\",\"id\":\"15\",\"open\":2,\"closed\":2}]}}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);
            ConversationAdminCount conversationAppCount = 
                conversationAdminCountJsonConverter.ReadJson(reader, typeof(ConversationAdminCount), null, null) as ConversationAdminCount;

            Assert.IsNotNull(conversationAppCount);
            Assert.AreEqual(3, conversationAppCount.admins.Count);
            Assert.IsTrue(conversationAppCount.admins.Any(a => a.name == "AAA" && a.open == 3));
        }

        [Test()]
        public void ReadJson_InvalidJson_ThrowsException()
        {
            String input = "{\"type\":\"count\",\"in\":[{\"name\":\"AAA\",\"id\":\"29\",\"open\":3,\"closed\":11},{\"name\":\"BBB\",\"id\":\"10\",\"open\":2,\"closed\":0},{\"name\":\"CCC\",\"id\":\"15\",\"open\":2,\"closed\":2}]}}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);

            Assert.Throws<JsonConverterException>(() =>
            {
                conversationAdminCountJsonConverter.ReadJson(reader, typeof(ConversationAdminCount), null, null);
            });
        }
    }
}