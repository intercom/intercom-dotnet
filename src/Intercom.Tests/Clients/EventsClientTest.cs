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
using System.Threading.Tasks;

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
		public async Task Create_WithNull_ThrowException()
		{
			await eventsClient.Create(null);
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public async Task List_NoIdOrUserIdOrEmail_ThrowException()
		{
			await eventsClient.List(new User());
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public async Task ListByParams_NoIdOrUserIdOrEmail_ThrowException()
		{
			await eventsClient.List(new Dictionary<String, String>());
		}
	}
}