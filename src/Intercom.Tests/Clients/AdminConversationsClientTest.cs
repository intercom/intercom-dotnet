using NUnit.Framework;
using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using Intercom.Factories;
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
            var auth = new Authentication(AppId, AppKey);
            var restClientFactory = new RestClientFactory(auth);
            adminConversationsClient = new AdminConversationsClient(restClientFactory);
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

        [Test()]
        public void ReplyLastConversation_NoReply_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => adminConversationsClient.ReplyLastConversation(new AdminLastConversationReply()));
        }
    }
}
