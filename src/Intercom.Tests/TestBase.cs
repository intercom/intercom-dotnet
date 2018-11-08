using System;
using Intercom.Core;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using Moq;
using Moq.Protected;
using System.IO;
using NUnit.Framework;

namespace Intercom.Test
{
    public class TestBase
    {
        protected String AppId = "DEFAULT";
        protected String AppKey = "DEFAULT";

        public TestBase()
        {
        }

        
        private static IRestClient MockRestClient<T>(HttpStatusCode httpStatusCode, string jsonFile) where T : new()
        {
            var json = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data", "Responses", jsonFile));
            var data = JsonConvert.DeserializeObject<T>(json);
            var response = new Mock<IRestResponse<T>>();
            response.Setup(_ => _.StatusCode).Returns(httpStatusCode);
            response.Setup(_ => _.Data).Returns(data);
            response.Setup(_ => _.Content).Returns(json);

            var mockIRestClient = new Mock<IRestClient>();
            mockIRestClient
                .Setup(x => x.Execute(It.IsAny<IRestRequest>()))
                .Returns(response.Object);

            return mockIRestClient.Object;
        }

        protected static TClient BuildMockClient<TClient, TResult>(HttpStatusCode httpStatusCode, string jsonResultFile, params object[] ctorArgs) where TResult : new() where TClient : Client
        {
            var result = MockRestClient<TResult>(httpStatusCode, jsonResultFile);
            var mock = new Mock<TClient>(ctorArgs) { CallBase = true };
            mock.Protected().Setup<IRestClient>("BuildClient").Returns(result);
            return mock.Object;
        }

        protected static TClient BuildSuccessMockClient<TClient, TResult>(string jsonResultFile, params object[] ctorArgs) where TResult : new() where TClient : Client
        {
            return BuildMockClient<TClient, TResult>(HttpStatusCode.OK, jsonResultFile, ctorArgs);
        }
    }
}