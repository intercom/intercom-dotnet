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
    public class UserConversationsClientTest  : TestBase
    {
        private UserConversationsClient userConversationsClient;

        public UserConversationsClientTest()
        {
            this.userConversationsClient = new UserConversationsClient(new Authentication(AppId, AppKey));
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
