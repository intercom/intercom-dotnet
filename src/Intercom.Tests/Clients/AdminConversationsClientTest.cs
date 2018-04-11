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
    // TODO: write tests for AdminConversationsClient
    [TestFixture()]
    public class AdminConversationsClientTest  : TestBase
    {
        private AdminConversationsClient adminConversationsClient;

        public AdminConversationsClientTest()
        {
            this.adminConversationsClient = new AdminConversationsClient(new Authentication(AppId, AppKey));
        }

        [Test()]
        public void Reply_WithNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => adminConversationsClient.Reply(null));
        }

        [Test()]
        public void Create_WithNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => adminConversationsClient.Create(null));
        }

        [Test()]
        public void List_NoId_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => adminConversationsClient.List(new Admin()));
        }
    }
}
