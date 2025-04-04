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
    internal class ArticlesClientTest : TestBase
    {
        private ArticlesClient articlesClient;

        public ArticlesClientTest()
            : base()
        {
            var auth = new Authentication(AppId, AppKey);
            var restClientFactory = new RestClientFactory(auth);
            articlesClient = new ArticlesClient(restClientFactory);
        }

        [Test()]
        public void View_WithEmptyString_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => articlesClient.View(String.Empty));
        }

        [Test()]
        public void View_NoId_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => articlesClient.View(new Article()));
        }

        [Test()]
        public void Create_WithNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => articlesClient.Create(null));
        }

        [Test()]
        public void GetArticles_WithNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => articlesClient.GetArticles(null));
        }

        [Test()]
        public void GetArticles_WithNoId_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => articlesClient.GetArticles(new Article()));
        }
    }
}
