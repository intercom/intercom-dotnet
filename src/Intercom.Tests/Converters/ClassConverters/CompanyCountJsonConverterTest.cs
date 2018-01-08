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
    public class CompanyCountJsonConverterTest : TestBase
    {
        private CompanyCountJsonConverter companyCountJsonConverter;

        public CompanyCountJsonConverterTest()
        {
            this.companyCountJsonConverter = new CompanyCountJsonConverter();
        }

        [Test()]
        public void ReadJson_ForCompanySegmentCount_ReturnsValidCount()
        {
            String input = "{\"type\":\"count\",\"company\":{\"segment\":[{\"New\":0},{\"Active\":1},{\"Slipping Away\":0}]}}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);
            CompanySegmentCount companySegmentCount = 
                companyCountJsonConverter.ReadJson(reader, typeof(CompanySegmentCount), null, null) as CompanySegmentCount;

            Assert.IsNotNull(companySegmentCount);
            Assert.AreEqual(3, companySegmentCount.segments.Count);
            Assert.AreEqual(0, companySegmentCount.segments["New"]);
        }

        [Test()]
        public void ReadJson_ForCompanyUserCount_ReturnsValidCount()
        {
            String input = "{\"type\":\"count\",\"company\":{\"user\":[{\"Test company\":2,\"remote_company_id\":\"2\"},{\"Test company 3\":1,\"remote_company_id\":\"3\"},{\"Serenity\":1,\"remote_company_id\":\"366\"},{\"Test company 4\":1,\"remote_company_id\":\"4\"}]}}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);
            CompanyUserCount companyUserCount = 
                companyCountJsonConverter.ReadJson(reader, typeof(CompanyUserCount), null, null) as CompanyUserCount; 

            Assert.IsNotNull(companyUserCount);
            Assert.AreEqual(4, companyUserCount.counts.Count);
            Assert.IsTrue(companyUserCount.counts.Any(c => c.name == "Test company" && c.count == 2));
        }

        [Test()]
        public void ReadJson_ForCompanyTagCount_ReturnsValidCompanyTag()
        {
            String input = "{\"type\":\"count\",\"company\":{\"tag\":[{\"automated-tag\":0},{\"cool-users-only\":6},{\"CSV Import - 2016-04-26 12:22:47 UTC\":0}]}}\n";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);

            CompanyTagCount companyTagCount = 
                companyCountJsonConverter.ReadJson(reader, typeof(CompanyTagCount), null, null) as CompanyTagCount; 
            
            Assert.IsNotNull(companyTagCount);
            Assert.AreEqual(3, companyTagCount.tags.Count );
            Assert.AreEqual(6, companyTagCount.tags["cool-users-only"]);
        }

        [Test()]
        public void ReadJson_InvalidJson_ThrowsException()
        {
            String input = "{\"type\":\"count\",\"company\"tag\":[{\"auto\"cool-users-only\":6},{\"CSV Import - 2016-04-26 12:22:47 UTC\":0}]}}\n";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);

            Assert.Throws<JsonConverterException>(() =>
            {
                companyCountJsonConverter.ReadJson(reader, typeof(CompanyTagCount), null, null);
            });
        }
    }
}