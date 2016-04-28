using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using RestSharp;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using RestSharp;
using RestSharp.Authenticators;
using System.IO;

namespace Library.Clients
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

        public Conversation View(String id, bool? displayAsPlainText = null)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("'id' argument is null or empty.");
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