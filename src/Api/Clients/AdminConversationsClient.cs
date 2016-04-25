using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using RestSharp;
using RestSharp.Authenticators;

namespace Library
{
    public class AdminConversationsClient: Client
    {

        public AdminConversationsClient(String baseUrl, String resource, Authentication authentication)
            : base(baseUrl, resource, authentication)
        {
        }

        public ConversationPart Reply(AdminConversationReply reply)
        {
            if (reply == null)
            {
                throw new ArgumentNullException("'reply' argument is null.");
            }

            ClientResponse<ConversationPart> result = null;
            String body = Serialize<AdminConversationReply>(reply);
            result = Post<ConversationPart>(body);
            return result.Result;
        }

        public AdminConversationMessage Create (AdminConversationMessage adminMessage)
        {
            ClientResponse<AdminConversationMessage> result = null;
            result = Post<AdminConversationMessage> (adminMessage);
            return result.Result;
        }

        public Conversations List(
            String adminId, 
            bool? open = null, 
            bool? displayAsPlainText = null)
        {
            if (String.IsNullOrEmpty(adminId))
            {
                throw new ArgumentNullException("'user' argument is null or empty.");
            }

            ClientResponse<Conversations> result = null;

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            parameters.Add(Constants.TYPE, Constants.ADMIN);

            if (open != null && open.HasValue)
            {
                parameters.Add(Constants.OPEN, open.Value.ToString());
            }

            if (displayAsPlainText != null && displayAsPlainText.HasValue)
            {
                parameters.Add(Constants.DISPLAY_AS, Constants.PLAIN_TEXT);
            }

            parameters.Add(Constants.ADMIN_ID, adminId);

            result = Get<Conversations>(parameters: parameters);
            return result.Result;
        }
    }
}