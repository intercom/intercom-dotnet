using NUnit.Framework;
using System;
using System.Net;
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

        public UserClientTest() { }

        private void SetupMock(RestClient restClient = null)
        {
            var auth = new Authentication(AppId, AppKey);
            if (restClient == null)
            {
                var restClientMock = new Mock<RestClient>();
                restClient = restClientMock.Object;
            }
            var restClientFactoryMock = new Mock<RestClientFactory>(new object[] { auth });
            restClientFactoryMock.Setup(x => x.RestClient).Returns(restClient);
            var restClientFactory = restClientFactoryMock.Object;
            usersClient = new UsersClient(restClientFactory);
        }

        [Test()]
        public void Create_WithNull_ThrowException()
        {
            SetupMock();
            Assert.Throws<ArgumentNullException>(() => usersClient.Create(null));
        }

        [Test()]
        public void Create_NoUserIdOrEmail_ThrowException()
        {
            SetupMock();
            Assert.Throws<ArgumentException>(() => usersClient.Create(new User()));
        }

        [Test()]
        public void Archive_NoIdOrUserIdOrEmail_ThrowException()
        {
            SetupMock();
            Assert.Throws<ArgumentException>(() => usersClient.Archive(new User()));
        }

        [Test()]
        public void PermanentlyDeleteUser_NoId_ThrowException()
        {
            SetupMock();
            Assert.Throws<ArgumentNullException>(() => usersClient.PermanentlyDeleteUser(null));
        }

        [Test()]
        public void Update_NoIdOrUserIdOrEmail_ThrowException()
        {
            SetupMock();
            Assert.Throws<ArgumentException>(() => usersClient.Update(new User()));
        }

        [Test()]
        public void View_ByStringId_ReturnsObjectAsExpected()
        {
            var userId = "id";
            var restClientMock = new Mock<RestClient>();
            var restResponse = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = $"{{ \"type\": \"user\", \"id\": \"530370b477ad7120001d\", \"user_id\": \"{userId}\", \"email\": \"wash@serenity.io\" }}",
            };
            restClientMock.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(restResponse);
            var restClient = restClientMock.Object;
            SetupMock(restClient);
            Assert.AreEqual(userId, usersClient.View(userId).user_id);
        }
    }
}
