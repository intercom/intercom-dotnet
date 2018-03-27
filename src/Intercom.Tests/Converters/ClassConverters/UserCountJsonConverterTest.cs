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
    public class UserCountJsonConverterTest : TestBase
    {
        private UserCountJsonConverter userCountJsonConverter;

        public UserCountJsonConverterTest()
        {
            this.userCountJsonConverter = new UserCountJsonConverter();
        }

        [Test()]
        public void ReadJson_ForUserSegment_ReturnsValidCount()
        {
            String input = "{\"type\":\"count\",\"user\":{\"segment\":[{\"Active\":2},{\"New\":0},{\"P1 Test\":0},{\"Slipping Away\":0}]}}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);
            UserSegmentCount userSegmentCount = 
                userCountJsonConverter.ReadJson(reader, typeof(UserSegmentCount), null, null) as UserSegmentCount;

            Assert.IsNotNull(userSegmentCount);
            Assert.AreEqual( 4, userSegmentCount.segments.Count);
            Assert.AreEqual(2, userSegmentCount.segments["Active"]);
        }

        [Test()]
        public void ReadJson_ForUserTag_ReturnsValidCount()
        {
            String input = "{\"type\":\"count\",\"user\":{\"tag\":[{\"automated-tag\":0},{\"has_device_token\":1001},{\"Tag 1\":1},{\"test_user_tagged\":2}]}}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);
            UserTagCount userTagCount = 
                userCountJsonConverter.ReadJson(reader, typeof(UserTagCount), null, null) as UserTagCount; 

            Assert.IsNotNull(userTagCount);
            Assert.AreEqual(4, userTagCount.tags.Count);
            Assert.IsTrue(userTagCount.tags.Any(c => c.Key == "has_device_token" && c.Value == 1001));
        }

        [Test()]
        public void ReadJson_InvalidJson_ThrowsException()
        {
            String input = "{\"type\":\"count\",\"usted-tag\":0},{\"has_device_token\":1001},{\"Tag 1\":1},{\"test_user_tagged\":2}]}}";
            StringReader stringReader = new StringReader(input);
            JsonReader reader = new JsonTextReader(stringReader);

            Assert.Throws<JsonConverterException>(() =>
            {
                userCountJsonConverter.ReadJson(reader, typeof(UserTagCount), null, null);
            });
        }
    }
}