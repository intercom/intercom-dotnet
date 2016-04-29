using NUnit.Framework;
using System;
using Library.Core;
using Library.Data;
using Library.Clients;
using Library.Exceptions;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using Newtonsoft.Json;
using Moq;

namespace Library.Test
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNull_ThrowException()
        {
            companyClient.Create(null);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_MoreThan100CustomAtt_ThrowException()
        {
            Dictionary<string, string> custom_attributes = new Dictionary<string, string>();

            for (int i = 0; i < 105; i++)
                custom_attributes.Add("field", "value");

            companyClient.Create(new Company() { custom_attributes = custom_attributes } );
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_CustomAttInvalidChars_ThrowException()
        {
            Dictionary<string, string> custom_attributes = new Dictionary<string, string>();
            custom_attributes.Add("invalid.ch$ar", "invalid");
            companyClient.Create(new Company() { custom_attributes = custom_attributes } );
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_CustomAttFieldLengthExceeded_ThrowException()
        {
            Dictionary<string, string> custom_attributes = new Dictionary<string, string>();
            custom_attributes.Add(new string('A', 200), "value");
            companyClient.Create(new Company() { custom_attributes = custom_attributes } );
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_CustomAttValueLengthExceeded_ThrowException()
        {
            Dictionary<string, string> custom_attributes = new Dictionary<string, string>();
            custom_attributes.Add("field", new string('A', 300));
            companyClient.Create(new Company() { custom_attributes = custom_attributes } );
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_WithNull_ThrowException()
        {
            companyClient.Update(null);
        }
    }
}