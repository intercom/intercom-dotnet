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
	public class NotesClientTest : TestBase
	{
		private NotesClient notesClient;

		public NotesClientTest()
		{
			this.notesClient = new NotesClient(new Authentication(AppId, AppKey));
		}

		[Test()]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task Create_WithNull_ThrowException()
		{
			await notesClient.Create(null);
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public async Task CreateWithNote_NoUserIdOrEmail_ThrowException()
		{
			await notesClient.Create(new Note() { user = new User() });
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public async Task CreateWithNote_NoBody_ThrowException()
		{
			await notesClient.Create(new Note() { user = new User() { email = "email@example.com" } });
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public async Task Create_NoUserIdOrEmail_ThrowException()
		{
			await notesClient.Create(new User(), String.Empty);
		}

		[Test()]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task Create_NoBody_ThrowException()
		{
			await notesClient.Create(new User() { email = "email@example.com" }, String.Empty);
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public async Task List_NoIdOrUserIdOrEmail_ThrowException()
		{
			await notesClient.List(new User());
		}
	}
}