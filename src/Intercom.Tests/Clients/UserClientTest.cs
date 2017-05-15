﻿using NUnit.Framework;
using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNull_ThrowException()
        {
            usersClient.Create(null);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_NoUserIdOrEmail_ThrowException()
        {
            usersClient.Create(new User());
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_NoIdOrUserIdOrEmail_ThrowException()
        {
            usersClient.Delete(new User());
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_NoIdOrUserIdOrEmail_ThrowException()
        {
            usersClient.Update(new User());
        }
    }
}