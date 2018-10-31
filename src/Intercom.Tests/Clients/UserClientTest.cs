using NUnit.Framework;
using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using Intercom.Factories;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using Newtonsoft.Json;
using Moq;

namespace Intercom.Test
{
    [TestFixture()]
    public class UserClientTest  : TestBase
    {
        private UsersClient usersClient;

        public UserClientTest()
        {
            var auth = new Authentication(AppId, AppKey);
            var restClientFactory = new RestClientFactory(auth);
            usersClient = new UsersClient(restClientFactory);
        }

        [Test()]
        public void Create_WithNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => usersClient.Create(null));
        }

        [Test()]
        public void Create_NoUserIdOrEmail_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => usersClient.Create(new User()));
        }

        [Test()]
        public void Archive_NoIdOrUserIdOrEmail_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => usersClient.Archive(new User()));
        }

        [Test()]
        public void PermanentlyDeleteUser_NoId_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => usersClient.PermanentlyDeleteUser(null));
        }

        [Test()]
        public void Update_NoIdOrUserIdOrEmail_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => usersClient.Update(new User()));
        }
    }
}
