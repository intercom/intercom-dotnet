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
        public void Create_WithNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => notesClient.Create(null));
        }

        [Test()]
        public void CreateWithNote_NoUserIdOrEmail_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => notesClient.Create(new Note() { user = new User() }));
        }

        [Test()]
        public void CreateWithNote_NoBody_ThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                notesClient.Create(new Note() { user = new User() { email = "email@example.com" } });
            });
        }

        [Test()]
        public void Create_NoUserIdOrEmail_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => notesClient.Create(new User(), String.Empty));
        }

        [Test()]
        public void Create_NoBody_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                notesClient.Create(new User() { email = "email@example.com" }, String.Empty);
            });
        }

        [Test()]
        public void List_NoIdOrUserIdOrEmail_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => notesClient.List(new User()));
        }
    }
}