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
	public class UserClientTest : TestBase
	{
		private UsersClient usersClient;

		public UserClientTest()
		{
			this.usersClient = new UsersClient(new Authentication(AppId, AppKey));
		}

		[Test()]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task Create_WithNull_ThrowException()
		{
			await usersClient.Create(null);
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public async Task Create_NoUserIdOrEmail_ThrowException()
		{
			await usersClient.Create(new User());
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public async Task Delete_NoIdOrUserIdOrEmail_ThrowException()
		{
			await usersClient.Delete(new User());
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public async Task Update_NoIdOrUserIdOrEmail_ThrowException()
		{
			await usersClient.Update(new User());
		}
	}
}