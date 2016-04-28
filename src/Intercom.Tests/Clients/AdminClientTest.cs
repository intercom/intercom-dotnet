using NUnit.Framework;
using System;
using Library.Core;
using Library.Data;
using Library.Clients;
using Library.Exceptions;
using RestSharp;
using RestSharp.Authenticators;
using Library.Test.Server;
using System.Collections.Generic;
using Newtonsoft.Json;
using Moq;

namespace Library.Test
{
    [TestFixture()]
    public class AdminClientTest : TestBase
    {
        private AdminsClient adminsClient;

        public AdminClientTest()
            : base()
        {
            this.adminsClient = new AdminsClient(new Authentication(AppId, AppKey));
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void View_WithEmptyString_ThrowException()
        {
            adminsClient.View(String.Empty);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void View_NoId_ThrowException()
        {
            adminsClient.View(new Admin());
        }
    }
}