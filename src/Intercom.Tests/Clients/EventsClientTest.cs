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
    public class EventsClientTest : TestBase
    {
        private EventsClient eventsClient;

        public EventsClientTest()
        {
            var auth = new Authentication(AppId, AppKey);
            var restClientFactory = new RestClientFactory(auth);
            eventsClient = new EventsClient(restClientFactory);
        }

        [Test()]
        public void Create_WithNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => eventsClient.Create(null));
        }

        [Test()]
        public void List_NoIdOrUserIdOrEmail_ThrowException()
        {
            Assert.Throws<ArgumentException>(() =>eventsClient.List(new User()));
        }

        [Test()]
        public void ListByParams_NoIdOrUserIdOrEmail_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => eventsClient.List(new Dictionary<String,String>()));
        }
    }
}