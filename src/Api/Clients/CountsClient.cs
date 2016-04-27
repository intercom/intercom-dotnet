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

        public AppCount GetAppCount()
        {
            ClientResponse<AppCount> result = null;
            result = Get<AppCount>();
            return result.Result;
        }

        public ConversationAdminCount GetConversationAdminCount()
        {
            Dictionary<String, String> parameters = new Dictionary<string, string>();
            parameters.Add(Constants.TYPE, Constants.CONVERSATION);
            parameters.Add(Constants.COUNT, Constants.ADMIN);

            ClientResponse<ConversationAdminCount> result = null;
            result = Get<ConversationAdminCount>(parameters: parameters);
            return result.Result;
        }
    }
}