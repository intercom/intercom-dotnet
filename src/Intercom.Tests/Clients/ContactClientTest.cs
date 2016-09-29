using NUnit.Framework;
using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNull_ThrowException()
        {
            contactsClient.Create(null);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_NoIdOrUserIdOrEmail_ThrowException()
        {
            contactsClient.Update(new Contact());
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ListByEmail_NoEmail_ThrowException()
        {
            contactsClient.List(String.Empty);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_NoIdOrUserIdOrEmail_ThrowException()
        {
            contactsClient.Delete(new Contact());
        }
    }
}