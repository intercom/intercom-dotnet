using NUnit.Framework;
using System;
using Intercom.Core;

namespace Intercom.Integration.Tests
{
    [TestFixture]
    public class Test : TestBase
    {

        [SetUp]
        public void SetUp()
        {
            CheckForApiCredentials();
        }

        [Test]
        public void Testing()
        {
        }
     
    }
}