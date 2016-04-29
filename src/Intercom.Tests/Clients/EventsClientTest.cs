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
    public class EventsClientTest : TestBase
    {
        private EventsClient eventsClient;

        public EventsClientTest()
        {
            this.eventsClient = new UserClient(new Authentication(AppId, AppKey));
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNull_ThrowException()
        {
            eventsClient.Create(null);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_NoUserIdOrEmail_ThrowException()
        {
            eventsClient.Create(new User());
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_NoIdOrUserIdOrEmail_ThrowException()
        {
            eventsClient.Delete(new User());
        }
    }
}