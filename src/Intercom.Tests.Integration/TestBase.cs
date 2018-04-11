using NUnit.Framework;
using System;
using Intercom.Core;

namespace Intercom.Integration.Tests
{
    [TestFixture()]
    public class TestBase
    {
        public String AppId;
        public String AppKey;

        public void CheckForApiCredentials()
        {
            this.AppKey = Environment.GetEnvironmentVariable("IntercomAppKey");
            this.AppId = Environment.GetEnvironmentVariable("IntercomAppId");

            if (String.IsNullOrEmpty(AppKey) || String.IsNullOrEmpty(AppId))
                Assert.Ignore("Intercom.Integration.Tests will be ignored because there are no environment variables defined for IntercomAppKey or IntercomAppId.");
        }

        public TestBase()
        {
        }
    }
}