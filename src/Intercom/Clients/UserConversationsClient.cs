using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intercom.Clients;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using RestSharp;
using RestSharp.Authenticators;

namespace Intercom.Clients
{
    public class UserConversationsClient: Client
    {
        private const String CONVERSATIONS_RESOURCE = "conversations";
        private const String MESSAGES_RESOURCE = "messages";
        private const String REPLY_RESOURCE = "reply";

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
                throw new ArgumentNullException(nameof(reply));
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
            User user, 
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

            if (!String.IsNullOrEmpty(user.id))
            {
                parameters.Add(Constants.INTERCOM_USER_ID, user.id);
                result = Get<Conversations>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(user.user_id))
            {
                parameters.Add(Constants.USER_ID, user.user_id);
                result = Get<Conversations>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(user.email))
            {
                parameters.Add(Constants.EMAIL, user.email);
                result = Get<Conversations>(parameters: parameters);            
            }
            else
            {
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to view a user.");
            }

            result = Get<Conversations>(parameters: parameters);
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