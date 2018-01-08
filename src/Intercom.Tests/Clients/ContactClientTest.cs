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
    public class ContactClientTest : TestBase
    {
        private ContactsClient contactsClient;

        public ContactClientTest()
            : base()
        {
            this.contactsClient = new ContactsClient(new Authentication(AppId, AppKey));
        }

        [Test()]
        public void Create_WithNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => contactsClient.Create(null));
        }

        [Test()]
        public void Update_NoIdOrUserIdOrEmail_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => contactsClient.Update(new Contact()));
        }

        [Test()]
        public void ListByEmail_NoEmail_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => contactsClient.List(String.Empty));
        }

        [Test()]
        public void Delete_NoIdOrUserIdOrEmail_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => contactsClient.Delete(new Contact()));
        }
    }
}