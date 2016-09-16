using System;
using System.Collections.Generic;
using Intercom.Clients;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;

namespace Intercom.Test
{
    [TestFixture()]
    public class TagsClientTest : TestBase
    {
        private TagsClient tagsClient;

        public TagsClientTest()
        {
            this.tagsClient = new TagsClient(new Authentication(AppId, AppKey));
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNull_ThrowException()
        {
            tagsClient.Create(null);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_NoIdOrName_ThrowException()
        {
            tagsClient.Create(new Tag());
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_WithNull_ThrowException()
        {
            Tag tag = null;
            tagsClient.Delete(tag);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_NoId_ThrowException()
        {
            tagsClient.Delete(new Tag());
        }


        [Test()]
        public void Tag_NoName_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Tag(String.Empty, new List<Company>());
                });

            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Tag(String.Empty, new List<User>());
                });

            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Tag(String.Empty, new List<Contact>());
                });
        }

        [Test()]
        public void Tag_NoIds_ThrowException()
        {
            List<Contact> contacts = null;
            List<User> users = null;
            List<Company> companies = null;

            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Tag("sample tag", companies);
                });
            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Tag("sample tag", contacts);
                });
            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Tag("sample tag", users);
                });
        }

        [Test()]
        public void TagWithEntityType_NoIds_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Tag("sample tag", null, TagsClient.EntityType.Company);
                });
            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Tag("sample tag", null, TagsClient.EntityType.User);
                });
            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Tag("sample tag", null, TagsClient.EntityType.Contact);
                });
        }

        [Test()]
        public void Untag_NoName_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Untag(String.Empty, new List<Company>());
                });

            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Untag(String.Empty, new List<User>());
                });

            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Untag(String.Empty, new List<Contact>());
                });
        }

        [Test()]
        public void Untag_NoIds_ThrowException()
        {
            List<Contact> contacts = null;
            List<User> users = null;
            List<Company> companies = null;

            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Untag("sample tag", companies);
                });
            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Untag("sample tag", contacts);
                });
            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Untag("sample tag", users);
                });
        }

        [Test()]
        public void UntagWithEntityType_NoIds_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Untag("sample tag", null, TagsClient.EntityType.Company);
                });
            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Untag("sample tag", null, TagsClient.EntityType.User);
                });
            Assert.Throws<ArgumentNullException>(() =>
                {
                    tagsClient.Untag("sample tag", null, TagsClient.EntityType.Contact);
                });
        }

    }
}