using NUnit.Framework;
using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNull_ThrowException()
        {
            eventsClient.Create(null);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void List_NoIdOrUserIdOrEmail_ThrowException()
        {
            eventsClient.List(new User());
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void ListByParams_NoIdOrUserIdOrEmail_ThrowException()
        {
            eventsClient.List(new Dictionary<String,String>());
        }
    }
}