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
    public class ConversationAppCountJsonConverterTest : TestBase
    {
        private ConversationAppCountJsonConverter conversationAppCountJsonConverter;

        public ConversationAppCountJsonConverterTest()
        {
            this.conversationAppCountJsonConverter = new ConversationAppCountJsonConverter();
        }

        [Test()]
        public void ReadJson_ForConversationAppCount_ReturnsValidCount()
        {
            String input = "{\"type\":\"count\",\"conversation\":{\"open\":24,\"closed\":35,\"unassigned\":11,\"assigned\":13}}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);
            ConversationAppCount conversationAppCount = 
                conversationAppCountJsonConverter.ReadJson(reader, typeof(ConversationAppCount), null, null) as ConversationAppCount;

            Assert.IsNotNull(conversationAppCount);
            Assert.AreEqual(13, conversationAppCount.assigned);
            Assert.AreEqual(11, conversationAppCount.unassigned);
            Assert.AreEqual(35, conversationAppCount.closed);
            Assert.AreEqual(24, conversationAppCount.open);
        }

        [Test()]
        public void ReadJson_InvalidJson_ThrowsException()
        {
            String input = "{\"type\":\"count\\pen\":24,\"closed\":35,\"unassigned\":11,\"assigned\":13}}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);

            Assert.Throws<JsonConverterException>(() =>
            {
                conversationAppCountJsonConverter.ReadJson(reader, typeof(ConversationAppCount), null, null);
            });
        }
    }
}