using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using System.Collections.Generic;

namespace Intercom.Data
{
    public class UserConversationReply : Reply
    {
        public string type
        {
            get
            {
                return Reply.ReplySenderType.USER;
            }
        }

        public String message_type
        { 
            get
            { 
                return Reply.ReplyMessageType.COMMENT; 
            }
        }

        public String email { set; get; }

        public String user_id { set; get; }

        public String intercom_user_id { set; get; }

        public UserConversationReply(
            String conversationId,
            String body,
            String intercomUserId = null, 
            String email = null, 
            String userId = null,
            List<String> attachementUrls = null)
            : base(conversationId, Reply.ReplyMessageType.COMMENT, body, attachementUrls)
        {

            if (String.IsNullOrEmpty(conversationId))
                throw new ArgumentNullException("conversation_id is null or empty.");
            
            if (String.IsNullOrEmpty(id) && String.IsNullOrEmpty(email) && String.IsNullOrEmpty(user_id))
                throw new ArgumentException("you need to provide either 'id', 'user_id', 'email' to view a user.");

            this.email = email;
            this.user_id = userId;
            this.intercom_user_id = intercomUserId;
        }
    }
}
