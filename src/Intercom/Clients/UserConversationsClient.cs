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
    public class UserConversationsClient: Client
    {
        private const String CONVERSATIONS_RESOURCE = "conversations";
        private const String MESSAGES_RESOURCE = "messages";
        private const String REPLY_RESOURCE = "r";

        public UserConversationsClient(Authentication authentication)
            : base(INTERCOM_API_BASE_URL, CONVERSATIONS_RESOURCE, authentication)
        {
        }

        public UserConversationsClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, CONVERSATIONS_RESOURCE, authentication)
        {
        }

        public ConversationPart Reply(UserConversationReply reply)
        {
            if (reply == null)
            {
                throw new ArgumentNullException("'reply' argument is null.");
            }

            ClientResponse<ConversationPart> result = null;
            String body = Serialize<UserConversationReply>(reply);
            result = Post<ConversationPart>(body, resource: CONVERSATIONS_RESOURCE + Path.DirectorySeparatorChar + reply.conversation_id + Path.DirectorySeparatorChar + REPLY_RESOURCE);
            return result.Result;
        }

        public UserConversationMessage Create(UserConversationMessage userMessage)
        {
            ClientResponse<UserConversationMessage> result = null;
            result = Post<UserConversationMessage>(userMessage, resource: MESSAGES_RESOURCE);
            return result.Result;
        }

        public Conversations List(
            String intercomUserId = null,
            String email = null,
            String userId = null, 
            bool? unread = null, 
            bool? displayAsPlainText = null)
        {
            ClientResponse<Conversations> result = null;

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            parameters.Add(Constants.TYPE, Constants.USER);

            if (unread != null && unread.HasValue)
            {
                parameters.Add(Constants.UNREAD, unread.Value.ToString());
            }

            if (displayAsPlainText != null && displayAsPlainText.HasValue)
            {
                parameters.Add(Constants.DISPLAY_AS, Constants.PLAIN_TEXT);
            }

            if (!String.IsNullOrEmpty(intercomUserId))
            {
                parameters.Add(Constants.INTERCOM_USER_ID, intercomUserId);
                result = Get<Conversations>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(userId))
            {
                parameters.Add(Constants.USER_ID, userId);
                result = Get<Conversations>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(email))
            {
                parameters.Add(Constants.EMAIL, email);
                result = Get<Conversations>(parameters: parameters);            
            }
            else
            {
                throw new ArgumentException("you need to provide either 'id', 'user_id', 'email' to view a user.");
            }

            result = Get<Conversations>(parameters: parameters);
            return result.Result;
        }
    }
}

