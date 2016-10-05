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
    public class NotesClientTest : TestBase
    {
        private NotesClient notesClient;

        public NotesClientTest()
        {
            this.notesClient = new NotesClient(new Authentication(AppId, AppKey));
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNull_ThrowException()
        {
            notesClient.Create(null);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateWithNote_NoUserIdOrEmail_ThrowException()
        {
            notesClient.Create(new Note() { user = new User() });
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateWithNote_NoBody_ThrowException()
        {
            notesClient.Create(new Note() { user = new User() { email = "email@example.com" } });
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_NoUserIdOrEmail_ThrowException()
        {
            notesClient.Create(new User(), String.Empty);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_NoBody_ThrowException()
        {
            notesClient.Create(new User() { email = "email@example.com" }, String.Empty);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void List_NoIdOrUserIdOrEmail_ThrowException()
        {
            notesClient.List(new User());
        }
    }
}