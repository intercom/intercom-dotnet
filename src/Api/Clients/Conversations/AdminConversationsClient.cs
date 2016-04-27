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
    public class AdminConversationsClient: Client
    {
        private const String CONVERSATIONS_RESOURCE = "conversations";
        private const String MESSAGES_RESOURCE = "messages";
        private const String REPLY_RESOURCE = "reply";

        public AdminConversationsClient(Authentication authentication)
            : base(INTERCOM_API_BASE_URL, CONVERSATIONS_RESOURCE, authentication)
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
            result = Post<ConversationPart>(body, resource: CONVERSATIONS_RESOURCE + Path.DirectorySeparatorChar + reply.conversation_id + Path.DirectorySeparatorChar + REPLY_RESOURCE);
            return result.Result;
        }

        public AdminConversationMessage Create(AdminConversationMessage adminMessage)
        {
            ClientResponse<AdminConversationMessage> result = null;
            result = Post<AdminConversationMessage>(adminMessage, resource: MESSAGES_RESOURCE);
            return result.Result;
        }

        public Conversations List(Admin admin, bool? open = null, bool? displayAsPlainText = null)
        {
            if (admin == null)
            {
                throw new ArgumentNullException("'admin' argument is null.");
            }

            if (String.IsNullOrEmpty(admin.id))
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

            parameters.Add(Constants.ADMIN_ID, admin.id);

            result = Get<Conversations>(parameters: parameters);
            return result.Result;
        }
    }
}