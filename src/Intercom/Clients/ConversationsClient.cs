using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using RestSharp;
using RestSharp.Authenticators;

namespace Intercom.Clients
{
    public class ConversationsClient : Client
    {
        public static class MessageType
        {
            public const String ASSIGNMENT = "assignment";
            public const String COMMENT = "comment";
            public const String CLOSE = "close";
            public const String OPEN = "open";
            public const String NOTE = "note";
        }

        private const String CONVERSATIONS_RESOURCE = "conversations";

        public ConversationsClient( Authentication authentication)
            : base(INTERCOM_API_BASE_URL, CONVERSATIONS_RESOURCE, authentication)
        {
        }

        public ConversationsClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, CONVERSATIONS_RESOURCE, authentication)
        {
        }

        public Conversation View(String id, bool? displayAsPlainText = null)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();

            if (displayAsPlainText != null && displayAsPlainText.HasValue)
            {
                parameters.Add(Constants.DISPLAY_AS, Constants.PLAIN_TEXT);
            }

            ClientResponse<Conversation> result = null;
            result = Get<Conversation>(resource: CONVERSATIONS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;
        }
    }
}