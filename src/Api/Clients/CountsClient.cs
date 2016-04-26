using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using RestSharp;
using RestSharp.Authenticators;
using System.IO;

namespace Library
{
    public class CountsClient: Client
    {
        private const String COUNTS_RESOURCE = "counts";

        public CountsClient (Authentication authentication)
            : base (INTERCOM_API_BASE_URL, COUNTS_RESOURCE, authentication)
        {
        }
    }
}