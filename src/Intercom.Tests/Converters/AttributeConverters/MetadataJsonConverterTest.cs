using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intercom.Clients;
using Intercom.Converters.AttributeConverters;
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
    public class MetadataJsonConverterTest : TestBase
    {
        private MetadataJsonConverter metadataJsonConverter;

        public MetadataJsonConverterTest()
        {
            this.metadataJsonConverter = new MetadataJsonConverter();
        }

        [Test()]
        public void ReadJson_ForConversationAppCount_ReturnsValidCount()
        {
            String input = "{\"number\":1000,\"string_1\":\"123123\",\"number_2\":1000,\"complex\":{\"amount\":3123123,\"currency\":\"aed\"},\"article_1\":{\"url\":\"https://example.org/orders/3434-3434\",\"value\":\"click here!\"}}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);
            Metadata metadata = metadataJsonConverter.ReadJson(reader, typeof(Metadata), null, null) as Metadata;

            Assert.AreEqual(5, metadata.GetMetadata().Count);
            Assert.AreEqual(1, metadata.GetRichLinks().Count);
            Assert.AreEqual(1, metadata.GetMonetaryAmounts().Count); 
            Assert.AreEqual("1000", metadata.GetMetadata("number"));
            Assert.AreEqual("123123", metadata.GetMetadata("string_1"));
        }

        [Test()]
        public void ReadJson_InvalidMonetaryAmount_ReturnsZeroAmount()
        {
            String input = "{\"complex\":{\"amount\":\"wrong_value\",\"currency\":\"aed\"},\"article_1\":{\"url\":\"https://example.org/orders/3434-3434\",\"value\":\"click here!\"}}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);

            Metadata metadata = metadataJsonConverter.ReadJson(reader, typeof(Metadata), null, null) as Metadata;
            Metadata.MonetaryAmount monetaryAmount = metadata.GetMonetaryAmount("complex");
            Assert.AreEqual(0, monetaryAmount.amount);
        }

        [Test()]
        public void ReadJson_InvalidJson_ThrowsException()
        {
            String input = "{\"complex\"\":\"aed\"},\"article_1\":{\"url\tps://example.org/orders/3434-3434\",\"value\":\"click here!\"}}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);

            Assert.Throws<JsonConverterException>(() => metadataJsonConverter.ReadJson(reader, typeof(Metadata), null, null));
        }
    }
}