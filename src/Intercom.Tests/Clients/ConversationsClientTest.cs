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
    public class ConversationsClientTest  : TestBase
    {
        private ConversationsClient conversationsClient;

        public ConversationsClientTest()
        {
            this.conversationsClient = new ConversationsClient(new Authentication(AppId, AppKey));
        }

        [Test()]
        public void View_WithNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => conversationsClient.View(null));
        }
    }
}