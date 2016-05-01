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
        [ExpectedException(typeof(ArgumentNullException))]
        public void Reply_WithNull_ThrowException()
        {
            adminConversationsClient.Reply(null);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNull_ThrowException()
        {
            adminConversationsClient.Create(null);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void List_NoId_ThrowException()
        {
            adminConversationsClient.List(new Admin());
        }
    }
}
