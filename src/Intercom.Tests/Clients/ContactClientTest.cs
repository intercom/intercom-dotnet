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
	public class ContactClientTest : TestBase
	{
		private ContactsClient contactsClient;

		public ContactClientTest()
			: base()
		{
			this.contactsClient = new ContactsClient(new Authentication(AppId, AppKey));
		}

		[Test()]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task Create_WithNull_ThrowException()
		{
			await contactsClient.Create(null);
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public async Task Update_NoIdOrUserIdOrEmail_ThrowException()
		{
			await contactsClient.Update(new Contact());
		}

		[Test()]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task ListByEmail_NoEmail_ThrowException()
		{
			await contactsClient.List(String.Empty);
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public async Task Delete_NoIdOrUserIdOrEmail_ThrowException()
		{
			await contactsClient.Delete(new Contact());
		}
	}
}