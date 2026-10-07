using System;
using System.IO;
using Intercom.Clients;
using Intercom.Core;
using Intercom.Exceptions;
using Intercom.Factories;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace Intercom.Test
{
    [TestFixture ()]
    public class ClientTest
    {
        private static IRestRequest CaptureRequest(Action<ContactsClient> call)
        {
            IRestRequest captured = null;
            var restClient = new Mock<IRestClient>();
            restClient.Setup(_ => _.Execute(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => captured = r)
                .Returns((IRestResponse)null);
            var factory = new Mock<RestClientFactory>(new Authentication("DEFAULT", "DEFAULT"));
            factory.Setup(_ => _.RestClient).Returns(restClient.Object);

            try { call(new ContactsClient(factory.Object)); }
            catch (IntercomException) { }

            return captured;
        }

        [Test]
        public void EncodesIdWhenBuildingPath ()
        {
            var request = CaptureRequest(c => c.View("a/b?x=1#f"));

            var uri = new RestClient("https://api.intercom.io/").BuildUri(request);
            Assert.AreEqual("/contacts/a%2Fb%3Fx%3D1%23f", uri.AbsolutePath);
            Assert.AreEqual(String.Empty, uri.Query);
        }

        [TestCase(".")]
        [TestCase("..")]
        public void RejectsDotSegmentId (string id)
        {
            Assert.Throws<ArgumentException>(() => CaptureRequest(c => c.View(id)));
        }
    }
}