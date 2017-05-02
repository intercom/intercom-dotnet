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
	public class ConversationsClientTest : TestBase
	{
		private ConversationsClient conversationsClient;

		public ConversationsClientTest()
		{
			this.conversationsClient = new ConversationsClient(new Authentication(AppId, AppKey));
		}

		[Test()]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task View_WithNull_ThrowException()
		{
			await conversationsClient.View(null);
		}
	}
}