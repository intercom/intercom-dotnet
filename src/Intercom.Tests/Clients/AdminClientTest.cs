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
        [ExpectedException(typeof(ArgumentException))]
        public void View_NoId_ThrowException()
        {
            adminsClient.View(new Admin());
        }
    }
}