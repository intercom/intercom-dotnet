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
    public class ListJonConverterTest : TestBase
    {
        private ListJsonConverter listJsonConverter;

        public ListJonConverterTest()
        {
            this.listJsonConverter = new ListJsonConverter();
        }

        [Test()]
        public void ReadJson_ForCompanyList_ReturnsValidCount()
        {
            String input = "{\"companies\":[{\"type\":\"company\",\"company_id\":\"first_company\",\"id\":\"57100\"},{\"type\":\"company\",\"company_id\":\"second_company\",\"id\":\"5800\"},{\"type\":\"company\",\"company_id\":\"third_company\",\"id\":\"5900\"}]}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);
            List<Company> companies = listJsonConverter.ReadJson(reader, typeof(List<Company>), null, null) as List<Company>;

            Assert.AreEqual(3, companies.Count);
            Assert.AreEqual("first_company", companies.First().company_id);
        }

        [Test()]
        public void ReadJson_InvalidSerializationType_ThrowsException()
        {
            String input = "{\"companies\":[{\"type\":\"company\",\"company_id\":\"first_company\",\"id\":\"57100\"},{\"type\":\"company\",\"company_id\":\"second_company\",\"id\":\"5800\"},{\"type\":\"company\",\"company_id\":\"third_company\",\"id\":\"5900\"}]}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);
            Assert.Throws<JsonConverterException>(() => listJsonConverter.ReadJson(reader, typeof(List<Segment>), null, null));
        }

        [Test()]
        public void ReadJson_InvalidJson_ThrowsException()
        {
            String input = "{\"compani\",\"company_id\":\"first_company\",\"id\":\"57100\"},{\"type\":\"company\",\"company_id\":\"second_company\",\"id\":\"5800\"},{\"type\":\"company\",\"company_id\":\"third_company\",\"id\":\"5900\"}]}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);

            Assert.Throws<JsonConverterException>(() => listJsonConverter.ReadJson(reader, typeof(List<Company>), null, null));
        }
    }
}