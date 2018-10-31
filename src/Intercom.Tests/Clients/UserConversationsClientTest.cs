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
    [TestFixture()]
    public class UserConversationsClientTest  : TestBase
    {
        private UserConversationsClient userConversationsClient;

        public UserConversationsClientTest()
        {
            var auth = new Authentication(AppId, AppKey);
            var restClientFactory = new RestClientFactory(auth);
            userConversationsClient = new UserConversationsClient(restClientFactory);
        }

        [Test()]
        public void Reply_WithNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => userConversationsClient.Reply(null));
        }

        [Test()]
        public void Create_WithNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => userConversationsClient.Create(null));
        }

        [Test()]
        public void List_NoIdOrUserIdOrEmail_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => userConversationsClient.List(new User()));
        }
    }
}
