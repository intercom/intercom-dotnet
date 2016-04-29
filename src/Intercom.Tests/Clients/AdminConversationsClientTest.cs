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
    public class AdminConversationsClientTest  : TestBase
    {
        private AdminConversationsClient adminConversationsClient;

        public AdminConversationsClientTest()
        {
            this.adminConversationsClient = new AdminConversationsClient(new Authentication(AppId, AppKey));
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNull_ThrowException()
        {
            //adminConversationsClient.Create(null);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_NoUserIdOrEmail_ThrowException()
        {
            //adminConversationsClient.Create(new User());
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_NoIdOrUserIdOrEmail_ThrowException()
        {
            //adminConversationsClient.Delete(new User());
        }

    }
}