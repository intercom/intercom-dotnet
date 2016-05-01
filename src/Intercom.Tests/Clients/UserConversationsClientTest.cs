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
    public class UserConversationsClientTest  : TestBase
    {
        private UserConversationsClient userConversationsClient;

        public UserConversationsClientTest()
        {
            this.userConversationsClient = new UserConversationsClient(new Authentication(AppId, AppKey));
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Reply_WithNull_ThrowException()
        {
            userConversationsClient.Reply(null);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNull_ThrowException()
        {
            userConversationsClient.Create(null);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void List_NoIdOrUserIdOrEmail_ThrowException()
        {
            userConversationsClient.List(new User());
        }
    }
}
