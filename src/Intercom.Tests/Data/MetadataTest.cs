using NUnit.Framework;
using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using RestSharp;
using System.Linq;
using RestSharp.Authenticators;
using System.Collections.Generic;
using Newtonsoft.Json;
using Moq;

namespace Intercom.Test
{
    [TestFixture()]
    public class MetadataTest  : TestBase
    {
        public MetadataTest()
        {
        }

        [Test()]
        public void Add_WithNullKey_ThrowException()
        {
            Metadata metadata = new Metadata();

            Assert.Throws<ArgumentNullException>(() =>
                {
                    object obj = null;
                    metadata.Add(String.Empty, obj);
                });

            Assert.Throws<ArgumentNullException>(() =>
                {
                    Metadata.RichLink richLink = null;
                    metadata.Add(String.Empty, richLink);
                });

            Assert.Throws<ArgumentNullException>(() =>
                {
                    Metadata.MonetaryAmount monetaryAmount = null;
                    metadata.Add(String.Empty, monetaryAmount);
                });
        }

        [Test()]
        public void GetMonetaryAmount_WithNullKey_ThrowException()
        {
            Metadata metadata = new Metadata();
            Assert.Throws<ArgumentNullException>(() => metadata.GetMonetaryAmount(null));
        }

        [Test()]
        public void GetRichLink_WithNullKey_ThrowException()
        {
            Metadata metadata = new Metadata();
            Assert.Throws<ArgumentNullException>(() => metadata.GetRichLink(null));
        }

        [Test()]
        public void GetRichLinks_NoParam_Returns10Entries()
        {
            Metadata m = new Metadata();

            for (int i = 0; i < 10; i++)
                m.Add(i.ToString(), new Metadata.RichLink(i.ToString(), i.ToString()));

            Assert.AreEqual(10, m.GetRichLinks().Count);
            Assert.IsTrue(m.GetRichLinks().Any(rl => rl.url == "1" && rl.value == "1"));
        }

        [Test()]
        public void GetMonetaryAmounts_NoParam_Returns10Entries()
        {
            Metadata m = new Metadata();

            for (int i = 0; i < 10; i++)
                m.Add(i.ToString(), new Metadata.MonetaryAmount(i, i.ToString()));

            Assert.AreEqual(10, m.GetMonetaryAmounts().Count);
            Assert.IsTrue(m.GetMonetaryAmounts().Any(rl => rl.amount == 1 && rl.currency == "1"));
        }

        [Test()]
        public void CheckMonetaryAmountEqualRef_NoParam_ShouldNotBeEqual()
        {
            Metadata m = new Metadata();
            Metadata.MonetaryAmount ma = new Metadata.MonetaryAmount(1, "1");

            m.Add("1", ma);
            Metadata.MonetaryAmount maResult = m.GetMonetaryAmount("1");

            Assert.AreNotEqual(ma, maResult);
            Assert.AreNotSame(ma, maResult);
        }

        [Test()]
        public void CheckRichLinkEqualRef_NoParam_ShouldNotBeEqual()
        {
            Metadata m = new Metadata();
            Metadata.RichLink rl = new Metadata.RichLink("1", "1");

            m.Add("1", rl);
            Metadata.RichLink rlResult = m.GetRichLink("1");

            Assert.AreNotEqual(rl, rlResult);
            Assert.AreNotSame(rl, rlResult);
        }

        [Test()]
        public void GetMetadata_NoParam_Returns10Entries()
        {
            Metadata m = new Metadata();

            for (int i = 0; i < 10; i++)
                m.Add(i.ToString(), i);

            Assert.AreEqual(10, m.GetMetadata().Count);
            Assert.IsTrue(m.GetMetadata().Any(md => md.Value.Equals(1)));
        }
    }
}