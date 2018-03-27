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
    public class UserClientTest  : TestBase
    {
        private UsersClient usersClient;

        public UserClientTest()
        {
            this.usersClient = new UsersClient(new Authentication(AppId, AppKey));
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
        public void Delete_NoIdOrUserIdOrEmail_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => usersClient.Delete(new User()));
        }

        [Test()]
        public void Update_NoIdOrUserIdOrEmail_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => usersClient.Update(new User()));
        }
    }
}