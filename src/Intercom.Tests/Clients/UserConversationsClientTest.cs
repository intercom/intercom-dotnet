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
	public class UserConversationsClientTest : TestBase
	{
		private UserConversationsClient userConversationsClient;

		public UserConversationsClientTest()
		{
			this.userConversationsClient = new UserConversationsClient(new Authentication(AppId, AppKey));
		}

		[Test()]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task Reply_WithNull_ThrowException()
		{
			await userConversationsClient.Reply(null);
		}

		[Test()]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task Create_WithNull_ThrowException()
		{
			await userConversationsClient.Create(null);
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public async Task List_NoIdOrUserIdOrEmail_ThrowException()
		{
			await userConversationsClient.List(new User());
		}
	}
}
