using NUnit.Framework;
using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using Newtonsoft.Json;
using Moq;

namespace Intercom.Test
{
    [TestFixture()]
    public class CompanyClientTest : TestBase
    {
        private CompanyClient companyClient;

        public CompanyClientTest()
        {
            this.companyClient = new CompanyClient(new Authentication(AppId, AppKey));
        }

        [Test()]
        public void Create_WithNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => companyClient.Create(null));
        }

        [Test()]
        public void Delete_MoreThan100CustomAtt_ThrowException()
        {
            Dictionary<string, object> custom_attributes = new Dictionary<string, object>();

            for (int i = 0; i < 105; i++)
                custom_attributes.Add("field", "value");

            Assert.Throws<ArgumentException>(() => 
            {
                companyClient.Create(new Company() { custom_attributes = custom_attributes });
            });
        }

        [Test()]
        public void Create_CustomAttInvalidChars_ThrowException()
        {
            Dictionary<string, object> custom_attributes = new Dictionary<string, object>();
            custom_attributes.Add("invalid.ch$ar", "invalid");
            Assert.Throws<ArgumentException>(() =>
            {
                companyClient.Create(new Company() { custom_attributes = custom_attributes });
            });
        }

        [Test()]
        public void Update_WithNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => companyClient.Update(null));
        }
    }
}