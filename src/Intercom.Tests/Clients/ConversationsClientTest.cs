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
    public class ConversationsClientTest  : TestBase
    {
        private ConversationsClient conversationsClient;

        public ConversationsClientTest()
        {
            this.conversationsClient = new ConversationsClient(new Authentication(AppId, AppKey));
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void View_WithNull_ThrowException()
        {
            conversationsClient.View(null);
        }
    }
}