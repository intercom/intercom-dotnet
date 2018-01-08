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
    public class EventsClientTest : TestBase
    {
        private EventsClient eventsClient;

        public EventsClientTest()
        {
            this.eventsClient = new EventsClient(new Authentication(AppId, AppKey));
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