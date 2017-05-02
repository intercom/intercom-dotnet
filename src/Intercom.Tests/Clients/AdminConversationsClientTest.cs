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
	// TODO: write tests for AdminConversationsClient
	[TestFixture()]
	public class AdminConversationsClientTest : TestBase
	{
		private AdminConversationsClient adminConversationsClient;

		public AdminConversationsClientTest()
		{
			this.adminConversationsClient = new AdminConversationsClient(new Authentication(AppId, AppKey));
		}

		[Test()]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task Reply_WithNull_ThrowException()
		{
			await adminConversationsClient.Reply(null);
		}

		[Test()]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task Create_WithNull_ThrowException()
		{
			await adminConversationsClient.Create(null);
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public async Task List_NoId_ThrowException()
		{
			await adminConversationsClient.List(new Admin());
		}
	}
}
