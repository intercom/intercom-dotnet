﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using Intercom.Factories;
using RestSharp;
using RestSharp.Authenticators;

namespace Intercom.Clients
{
    public class CountsClient: Client
    {
        private const String COUNTS_RESOURCE = "counts";

        public CountsClient (RestClientFactory restClientFactory)
            : base (COUNTS_RESOURCE, restClientFactory)
        {
        }

        [Obsolete("This constructor is deprecated as of 3.0.0 and will soon be removed, please use CountsClient(RestClientFactory restClientFactory)")]
        public CountsClient(Authentication authentication)
            : base(INTERCOM_API_BASE_URL, COUNTS_RESOURCE, authentication)
        {
        }

        [Obsolete("This constructor is deprecated as of 3.0.0 and will soon be removed, please use CountsClient(RestClientFactory restClientFactory)")]
        public CountsClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, COUNTS_RESOURCE, authentication)
        {
        }

        public AppCount GetAppCount()
        {
            ClientResponse<AppCount> result = null;
            result = Get<AppCount>();
            return result.Result;
        }

        public ConversationAppCount GetConversationAppCount()
        {
            Dictionary<String, String> parameters = new Dictionary<string, string>();
            parameters.Add(Constants.TYPE, Constants.CONVERSATION);

            ClientResponse<ConversationAppCount> result = null;
            result = Get<ConversationAppCount>(parameters: parameters);
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

        public UserTagCount GetUserTagCount()
        {
            Dictionary<String, String> parameters = new Dictionary<string, string>();
            parameters.Add(Constants.TYPE, Constants.USER);
            parameters.Add(Constants.COUNT, Constants.TAG);

            ClientResponse<UserTagCount> result = null;
            result = Get<UserTagCount>(parameters: parameters);
            return result.Result;
        }

        public UserSegmentCount GetUserSegmentCount()
        {
            Dictionary<String, String> parameters = new Dictionary<string, string>();
            parameters.Add(Constants.TYPE, Constants.USER);
            parameters.Add(Constants.COUNT, Constants.SEGMENT);

            ClientResponse<UserSegmentCount> result = null;
            result = Get<UserSegmentCount>(parameters: parameters);
            return result.Result;
        }

        public CompanySegmentCount GetCompanySegmentCount()
        {
            Dictionary<String, String> parameters = new Dictionary<string, string>();
            parameters.Add(Constants.TYPE, Constants.COMPANY);
            parameters.Add(Constants.COUNT, Constants.SEGMENT);

            ClientResponse<CompanySegmentCount> result = null;
            result = Get<CompanySegmentCount>(parameters: parameters);
            return result.Result;
        }

        public CompanyTagCount GetCompanyTagCount()
        {
            Dictionary<String, String> parameters = new Dictionary<string, string>();
            parameters.Add(Constants.TYPE, Constants.COMPANY);
            parameters.Add(Constants.COUNT, Constants.TAG);

            ClientResponse<CompanyTagCount> result = null;
            result = Get<CompanyTagCount>(parameters: parameters);
            return result.Result;
        }

        public CompanyUserCount GetCompanyUserCount()
        {
            Dictionary<String, String> parameters = new Dictionary<string, string>();
            parameters.Add(Constants.TYPE, Constants.COMPANY);
            parameters.Add(Constants.COUNT, Constants.USER);

            ClientResponse<CompanyUserCount> result = null;
            result = Get<CompanyUserCount>(parameters: parameters);
            return result.Result;
        }
    }
}