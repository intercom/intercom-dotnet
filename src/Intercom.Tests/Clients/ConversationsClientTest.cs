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
    public class ConversationsClientTest  : TestBase
    {
        private ConversationsClient conversationsClient;

        public ConversationsClientTest()
        {
            var auth = new Authentication(AppId, AppKey);
            var restClientFactory = new RestClientFactory(auth);
            conversationsClient = new ConversationsClient(restClientFactory);
        }

        [Test()]
        public void View_WithNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => conversationsClient.View(null));
        }

        [Test()]
        public void ListAll_WithNullParameters_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => conversationsClient.ListAll(null));
        }

    }
}
